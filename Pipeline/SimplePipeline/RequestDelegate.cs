using System.Threading.Tasks;

namespace SimplePipeline
{
    public delegate Task RequestDelegate(HttpContext context);
}
