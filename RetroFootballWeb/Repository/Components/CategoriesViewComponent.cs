using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;

namespace RetroFootballWeb.Repository.Components
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly DataContext _context;
        public CategoriesViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            Category category = new Category();
            if (!String.IsNullOrEmpty(type)) category.Name = type;
            if (type == "Club")
            {
                category.Value = await _context.Products
                    .Where(product => product.Club != "None")
                    .Select(product => product.Club)
                    .Distinct().ToListAsync();
            }
            else
            if (type == "Nation")
            {
                category.Value = await _context.Products
                    .Where(product => product.Nation != "None")
                    .Select(product => product.Nation)
                    .Distinct().ToListAsync();
            }
            else
            category.Value = await _context.Products
                    .Select(product => product.Season)
                    .Distinct().ToListAsync();
            return View(category);
        }
    }
}
