using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC
{
    public class DefaultController : Controller
    {
        [HttpGet("/Default/{name}")]
        public IActionResult Index(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}
