using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimplePipeline
{
    public interface IWebHost
    {
        void Start();
    }

    public class WebHost : IWebHost
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public WebHost(IServiceCollection services, IConfiguration config)
        {
            _serviceProvider = services.BuildServiceProvider();
            _config = config;
        }

        public void Start()
        {
            IApplicationBuilder applicationBuilder = _serviceProvider.GetRequiredService<IApplicationBuilder>();
            _serviceProvider.GetRequiredService<IStartup>().Configure(applicationBuilder);

            IServer server = _serviceProvider.GetRequiredService<IServer>();
            IServerAddressesFeature addressesFeatures = server.Features.Get<IServerAddressesFeature>();

            string addresses = _config["ServerAddresses"] ?? "http://localhost:5000";
            foreach (string address in addresses.Split(';'))
            {
                addressesFeatures.Addresses.Add(address);
            }

            server.Start(new HostingApplication(applicationBuilder.Build()));
        }
    }
}
