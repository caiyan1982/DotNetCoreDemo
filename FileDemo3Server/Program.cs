/*********
* 
*   from https://www.cnblogs.com/artech/p/net-core-file-provider-05.html
*   
*********/
using FileDemo3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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
