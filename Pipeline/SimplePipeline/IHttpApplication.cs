using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimplePipeline
{
    public interface IHttpApplication<TContext>
    {
        TContext CreateContext(IFeatureCollection contextFeatures);
        Task ProcessRequestAsync(TContext context);
        void DisposeContext(TContext context, Exception exception);
    }

    public class HostingApplication : IHttpApplication<Context>
    {
        public RequestDelegate Application { get; }

        public HostingApplication(RequestDelegate application)
        {
            Application = application;
        }

        public Context CreateContext(IFeatureCollection contextFeatures)
        {
            HttpContext context = new DefaultHttpContext(contextFeatures);
            return new Context
            {
                HttpContext = context,
                StartTimestamp = Stopwatch.GetTimestamp()
            };
        }

        public Task ProcessRequestAsync(Context context) => Application(context.HttpContext);

        public void DisposeContext(Context context, Exception exception) => context.Scope?.Dispose();
    }

    public class Context
    {
        public HttpContext HttpContext { get; set; }
        public IDisposable Scope { get; set; }
        public long StartTimestamp { get; set; }
    }
}
