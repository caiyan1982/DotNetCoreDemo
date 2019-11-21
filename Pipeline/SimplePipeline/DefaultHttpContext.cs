using System;
using System.IO;

namespace SimplePipeline
{
    public class DefaultHttpContext : HttpContext
    {
        public IFeatureCollection HttpContextFeatures { get; }

        public override HttpRequest Request { get; }
        public override HttpResponse Response { get; }

        public DefaultHttpContext(IFeatureCollection httpContextFeatures)
        {
            HttpContextFeatures = httpContextFeatures;
            Request = new DefaultHttpRequest(this);
            Response = new DefaultHttpResponse(this);
        }
    }

    public class DefaultHttpRequest : HttpRequest
    {
        public IHttpRequestFeature RequestFeature { get; }

        public override Uri Url
        {
            get { return RequestFeature.Url; }
        }

        public override string PathBase
        {
            get { return RequestFeature.PathBase; }
        }

        public DefaultHttpRequest(DefaultHttpContext context)
        {
            RequestFeature = context.HttpContextFeatures.Get<IHttpRequestFeature>();
        }
    }

    public class DefaultHttpResponse : HttpResponse
    {
        public IHttpResponseFeature ResponseFeature { get; }

        public override Stream OutputStream
        {
            get { return ResponseFeature.OutputStream; }
        }

        public override string ContentType
        {
            get { return ResponseFeature.ContextType; }
            set { ResponseFeature.ContextType = value; }
        }

        public override int StatusCode
        {
            get { return ResponseFeature.StatusCode; }
            set { ResponseFeature.StatusCode = value; }
        }

        public DefaultHttpResponse(DefaultHttpContext context)
        {
            ResponseFeature = context.HttpContextFeatures.Get<IHttpResponseFeature>();
        }
    }

    public interface IHttpRequestFeature
    {
        Uri Url { get; }
        string PathBase { get; }
    }

    public interface IHttpResponseFeature
    {
        Stream OutputStream { get; }
        string ContextType { get; set; }
        int StatusCode { get; set; }
    }
}
