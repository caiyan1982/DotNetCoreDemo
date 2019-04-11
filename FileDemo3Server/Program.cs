using FileDemo3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace FileDemo3Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .Configure(app => app.UseMiddleware<FileProviderMiddleware>(@"F:\Test"))
                .Build()
                .Run();
        }
    }
}
