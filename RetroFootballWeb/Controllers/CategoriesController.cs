using Microsoft.AspNetCore.Mvc;

namespace RetroFootballWeb.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
