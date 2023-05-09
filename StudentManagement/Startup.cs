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
        //��������ע�뷽ʽ��ȡappsetting.json�ļ��еļ�ֵ
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); //����404�Ҳ�����ҳ����Ϣ
            }
            //��Ӷ�ȡ��̬�ļ��м��
            app.UseStaticFiles();
            //ʹ��Ĭ��·��
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route => {
                route.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
            });
            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";              
            //    await context.Response.WriteAsync("��ǰ��������:"+env.EnvironmentName);
            //});
        }


        public void Configure1(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("��ʼִ��Configure����");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //���Ĭ���ļ��м��(����index.html��index.htm��default.html��default.htm)
            //app.UseDefaultFiles();

            //�޸�Ĭ���ļ�
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("MyPage.html");
            //app.UseDefaultFiles(defaultFilesOptions);

            //����Ŀ¼����м��
            //app.UseDirectoryBrowser();

            //��Ӷ�ȡ��̬�ļ��м��
            app.UseStaticFiles();

            //������app.UseStaticFiles()��app.UseDefaultFiles()�м��
            //app.UseFileServer();
            //�޸�Ĭ���ļ�
            //FileServerOptions options = new FileServerOptions();
            //options.DefaultFilesOptions.DefaultFileNames.Clear();
            //options.DefaultFilesOptions.DefaultFileNames.Add("MyPage.html");
            //app.UseFileServer(options);


            app.Use(async (context, next) =>
            {
                await next();//��ת����һ���м��
            });

            app.Run(async (context) =>
            {

                context.Response.ContentType = "text/plain;charset=utf-8";

                //��ȡ��ǰ������
                //var proName= System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //await context.Response.WriteAsync(proName);

                //��ȡappsetting�е�MyKey��ֵ
                //string myKey=Configuration["MyKey"];
                //await context.Response.WriteAsync(myKey);

                //await context.Response.WriteAsync("��Ŀ������һ��ҳ��");                
                await context.Response.WriteAsync("��ǰ��������:" + env.EnvironmentName);
            });
        }
    }
}
