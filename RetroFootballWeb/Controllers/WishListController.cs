using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class WishListController : Controller
    {
        private readonly DataContext _context;
        private UserManager<AppUser> _userManager;
        public WishListController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var wishLists = await _context.WishLists.Where(w => w.CustomerId == customerID).ToListAsync();

            foreach (var item in wishLists)
            {
                item.Customer = await _context.Customers.FindAsync(customerID);
                item.Product = await _context.Products.FindAsync(item.ProductId);
            }

            return View(wishLists);
        }
        public async Task<IActionResult> AddToWishList(string productID)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var item = await _context.WishLists.FindAsync(customerID, productID);

            if (item != null)
            {
                TempData["SuccessMessage"] = "This item has been added to wish list already!";
            }
            else
            {
                var newItem = new WishList
                {
                    CustomerId = customerID,
                    ProductId = productID,
                    Product = await _context.Products.FindAsync(productID),
                    Customer = await _context.Customers.FindAsync(customerID)
                };

                _context.WishLists.Add(newItem);

                TempData["SuccessMessage"] = "Add to wish list successfully!";

                _context.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Remove(string productID)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var item = await _context.WishLists.FindAsync(customerID, productID);
            if (item != null)
            {
                _context.WishLists.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
