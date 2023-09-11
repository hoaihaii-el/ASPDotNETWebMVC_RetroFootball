using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(string productID = "")
        {
            var product = _context.Products.Where(p => p.ID == productID).FirstOrDefault();
            return View(product);
        }
    }
}
