using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class YourOrdersController : Controller
    {
        private DataContext _context;
        private UserManager<AppUser> _userManager;
        public YourOrdersController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "YourOrders";

            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            var orders = await _context.Orders.Where(o => o.CustomerID == user.Id).ToListAsync();
            foreach (Order order in orders)
            {
                order.Customer = await _context.Customers.FindAsync(user.Id);
            }

            return View(orders);
        }
        public async Task<IActionResult> Detail(int orderID)
        {
            var orders = await _context.OrderDetails.Where(o => o.OrderID == orderID).ToListAsync();
            foreach (OrderDetail order in orders)
            {
                order.Product = await _context.Products.FindAsync(order.ProductID);
            }
            return View(orders);
        }
    }
}
