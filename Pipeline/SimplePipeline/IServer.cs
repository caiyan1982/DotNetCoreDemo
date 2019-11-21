using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SimplePipeline
{
    public interface IServer
    {
        IFeatureCollection Features { get; }
        void Start<TContext>(IHttpApplication<TContext> application);
    }

    public class HttpListenerServer : IServer
    {
        public HttpListener Listener { get; }
        public IFeatureCollection Features { get; }

        public HttpListenerServer()
        {
            Listener = new HttpListener();
            Features = new FeatureCollection()
                .Set<IServerAddressesFeature>(new ServerAddressesFeature());
        }

        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            IServerAddressesFeature addressFeatures = Features.Get<IServerAddressesFeature>();
            foreach (string address in addressFeatures.Addresses)
            {
                Listener.Prefixes.Add(address.TrimEnd('/') + "/");
            }

            Listener.Start();
            while (true)
            {
                HttpListenerContext httpListenerContext = Listener.GetContext();
                HttpListenerContextFeature feature = new HttpListenerContextFeature(httpListenerContext, Listener);
                IFeatureCollection contextFeatures = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);
                TContext context = application.CreateContext(contextFeatures);

                application.ProcessRequestAsync(context)
                    .ContinueWith(_ => httpListenerContext.Response.Close())
                    .ContinueWith(_ => application.DisposeContext(context, _.Exception));
            }
        }
    }

    public class HttpListenerContextFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _context;

        public Uri Url { get; }

        public string PathBase { get; }

        public Stream OutputStream { get; }

        public string ContextType
        {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public int StatusCode
        {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }

        public HttpListenerContextFeature(HttpListenerContext context, HttpListener listener)
        {
            _context = context;
            Url = context.Request.Url;
            OutputStream = context.Response.OutputStream;
            PathBase = (from it in listener.Prefixes
                        let pathBase = new Uri(it).LocalPath.TrimEnd('/')
                        where context.Request.Url.LocalPath.StartsWith(pathBase, StringComparison.OrdinalIgnoreCase)
                        select pathBase).First();
        }
    }
}
