using Microsoft.AspNetCore.Mvc;

namespace RetroFootballWeb.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
