using Microsoft.AspNetCore.Mvc;

namespace RetroFootballWeb.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
