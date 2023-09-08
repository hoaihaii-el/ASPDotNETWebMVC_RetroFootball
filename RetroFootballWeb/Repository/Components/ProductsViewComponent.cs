using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RetroFootballWeb.Repository.Components
{
    public class ProductsViewComponent: ViewComponent
    {
        private readonly DataContext _context;
        public ProductsViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string type = "")
        {
            if (String.IsNullOrEmpty(type)) return View(await _context.Products.Take(4).ToListAsync());
            if (type == "BestSeller")
            {
                //logic for best seller product here
                return View(await _context.Products.Take(4).ToListAsync());
            }
            else
            if (type == "JustArrived")
            {
                //logic for just arrived product here
                return View(await _context.Products.Take(4).ToListAsync());
            }
            else
            {
                //logic for recommend product here
                return View(await _context.Products.Take(4).ToListAsync());
            }
        }
    }
}
