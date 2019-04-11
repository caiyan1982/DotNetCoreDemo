using Microsoft.AspNetCore.Hosting;

namespace HelloWorldWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000", "http://localhost:5001")
                //.UseContentRoot(Directory.GetCurrentDirectory())
                //.ConfigureLogging((hostingContext, logging) => { logging.AddConsole(); })
                .UseStartup<Startup>().Build();
    }
}
