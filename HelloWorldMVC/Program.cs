using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace HelloWorldMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build()
                .Run();
        }
    }
}
