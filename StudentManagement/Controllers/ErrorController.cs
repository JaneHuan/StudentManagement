using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    public class ErrorController: Controller
    {
        private ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeReExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "抱歉，你访问的页面不存在";

                    ViewBag.Path=statusCodeReExecute.OriginalPath;
                    ViewBag.QueryString= statusCodeReExecute.OriginalQueryString;
                    _logger.LogWarning($"路径:{statusCodeReExecute.OriginalPath}中查询字符串:{statusCodeReExecute.OriginalQueryString}找不到");

                    break;
            }
            return View("NotFound");
        }
        [AllowAnonymous]//允许匿名访问(即不需要登录也可以访问)
        [Route("Error")]
        public IActionResult Error()
        {
           var exceptionHandle=  HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"路径:{exceptionHandle.Path}发生了异常:{ exceptionHandle.Error.Message};{exceptionHandle.Error.StackTrace}");
            //ViewBag.ExceptionPath = exceptionHandle.Path;//异常路径
            //ViewBag.ErrorMessage= exceptionHandle.Error.Message;//异常信息
            //ViewBag.ErrorStackTrace= exceptionHandle.Error.StackTrace;// 异常堆栈
            return View();
        }
    }
}
