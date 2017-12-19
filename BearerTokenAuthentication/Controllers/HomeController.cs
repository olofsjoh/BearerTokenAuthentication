using Microsoft.AspNetCore.Mvc;

namespace BearerTokenAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}