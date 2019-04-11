using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController
    {
        // GET api/values/Tom
        [HttpGet("{name}")]
        public string Get(string name)
        {
            return $"Hello {name}";
        }
    }
}
