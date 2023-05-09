using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //用于依赖注入方式获取appsetting.json文件中的键值
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("StudentDBConnection"))
                ) ;
            services.AddMvc().AddRazorOptions(options => options.AllowRecompilingViewsOnFileChange = true);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IStudentRepository, SQLStudentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //拦截404找不到的页面信息
            }
            //添加读取静态文件中间件
            app.UseStaticFiles();
            //使用默认路由
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route => {
                route.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";              
            //    await context.Response.WriteAsync("当前环境变量:"+env.EnvironmentName);
            //});
        }


        public void Configure1(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("开始执行Configure方法");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //添加默认文件中间件(包括index.html、index.htm、default.html、default.htm)
            //app.UseDefaultFiles();

            //修改默认文件
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("MyPage.html");
            //app.UseDefaultFiles(defaultFilesOptions);

            //启用目录浏览中间件
            //app.UseDirectoryBrowser();

            //添加读取静态文件中间件
            app.UseStaticFiles();

            //涵盖了app.UseStaticFiles()和app.UseDefaultFiles()中间件
            //app.UseFileServer();
            //修改默认文件
            //FileServerOptions options = new FileServerOptions();
            //options.DefaultFilesOptions.DefaultFileNames.Clear();
            //options.DefaultFilesOptions.DefaultFileNames.Add("MyPage.html");
            //app.UseFileServer(options);


            app.Use(async (context, next) =>
            {
                await next();//跳转到下一个中间件
            });

            app.Run(async (context) =>
            {

                context.Response.ContentType = "text/plain;charset=utf-8";

                //获取当前进程名
                //var proName= System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //await context.Response.WriteAsync(proName);

                //读取appsetting中的MyKey的值
                //string myKey=Configuration["MyKey"];
                //await context.Response.WriteAsync(myKey);

                //await context.Response.WriteAsync("项目启动了一个页面");                
                await context.Response.WriteAsync("当前环境变量:" + env.EnvironmentName);
            });
        }
    }
}
