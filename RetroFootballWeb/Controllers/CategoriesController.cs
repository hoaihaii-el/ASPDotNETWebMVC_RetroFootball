using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Models.ViewModels;
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
        public async Task<IActionResult> Index(string type = "", string value = "", int pg = 1)
        {
            List<Product> products = new List<Product>();
            const int pageSize = 8;

            ViewData["cateType"] = type;
            ViewData["cateValue"] = value;

            ViewData["Active"] = "Categories";
            if (type == "Club")
            {
                products = await _context.Products
                    .Where(p => p.Club == value)
                    .Select(p => p).ToListAsync();
            }
            else
            if (type == "Nation")
            {
                products = await _context.Products
                    .Where(p => p.Nation == value)
                    .Select(p => p).ToListAsync();
            }
            else
            if (type == "Season")
            {
                products = await _context.Products
                        .Where(p => p.Season == value)
                        .Select(p => p).ToListAsync();
            }
            else products = await _context.Products.ToListAsync();

            if (pg < 1) pg = 1;
            int recsCount = products.Count();
            var pager = new PagingInfo(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = products.Skip(recSkip).Take(pageSize).ToList();
            ViewData["Pager"] = pager;

            return View(data);
        }
    }
}
