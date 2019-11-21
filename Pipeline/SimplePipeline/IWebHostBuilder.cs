using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimplePipeline
{
    public interface IWebHostBuilder
    {
        IWebHost Build();
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);
        IWebHostBuilder UseSetting(string key, string value);
    }

    public class WebHostBuilder : IWebHostBuilder
    {
        private IServiceCollection _services;
        private IConfiguration _config;

        public WebHostBuilder()
        {
            _services = new ServiceCollection().AddSingleton<IApplicationBuilder, ApplicationBuilder>();
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
        }

        public IWebHost Build() => new WebHost(_services, _config);

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            configureServices(_services);
            return this;
        }

        public IWebHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }
    }
}
