using Microsoft.AspNetCore.Mvc;

namespace BitstampApp.Api.Controllers
{
    [Route("[controller]/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
