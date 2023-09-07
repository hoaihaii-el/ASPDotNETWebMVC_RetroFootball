using Microsoft.AspNetCore.Mvc;

namespace RetroFootballWeb.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
