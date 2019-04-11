using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace FileDemo3Server
{
    public class TimeRecorderMiddleware
    {
        RequestDelegate _next;

        public TimeRecorderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();


            await _next(context);

            var newDiv = @"1234页面处理时间：{0} 毫秒";
            var text = string.Format(newDiv, sw.ElapsedMilliseconds);
            await context.Response.WriteAsync(text);
        }
    }
}
