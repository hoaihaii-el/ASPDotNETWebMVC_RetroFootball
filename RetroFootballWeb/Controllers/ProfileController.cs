using Microsoft.AspNetCore.Mvc;

namespace RetroFootballWeb.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Active"] = "Profile";
            return View();
        }
    }
}
