using System;

namespace SimplePipeline
{
    public interface IStartup
    {
        void Configure(IApplicationBuilder app);
    }

    public class DelegateStartup : IStartup
    {
        private Action<IApplicationBuilder> _configura;

        public DelegateStartup(Action<IApplicationBuilder> configura)
        {
            _configura = configura;
        }

        public void Configure(IApplicationBuilder app)
        {
            _configura(app);
        }
    }
}
