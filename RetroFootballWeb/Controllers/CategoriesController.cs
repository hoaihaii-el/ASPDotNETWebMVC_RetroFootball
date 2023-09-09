using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;
        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string type = "", string value = "")
        {
            ViewData["Active"] = "Categories";
            if (type == "Club")
            {
                return View(await _context.Products
                    .Where(p => p.Club == value)
                    .Select(p => p).ToListAsync());
            }
            else
            if (type == "Nation")
            {
                return View(await _context.Products
                    .Where(p => p.Nation == value)
                    .Select(p => p).ToListAsync());
            }
            else
            if (type == "Season")
            {
                return View(await _context.Products
                        .Where(p => p.Season == value)
                        .Select(p => p).ToListAsync());
            }
            else return View(await _context.Products.Take(8).ToListAsync());
        }
    }
}
