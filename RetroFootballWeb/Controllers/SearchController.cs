using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Models.ViewModels;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataContext _context;
        public SearchController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string search = "", int pg = 1)
        {
            ViewData["Search"] = search;

            List<Product> products = new List<Product>();

            if (String.IsNullOrEmpty(search)) products = await _context.Products.ToListAsync();
            products = await _context.Products.Where(p => p.Name.Contains(search)).ToListAsync();

            const int pageSize = 8;
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
