using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetroFootballWeb.Models;
using RetroFootballWeb.Models.ViewModels;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _context;
        private UserManager<AppUser> _userManager;
        public CheckoutController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            // Get info cart
            List<CartItem> mainCarts = new List<CartItem>();
            var cartItems = _context.Carts
            .Where(cart => cart.CustomerId == user.Id)
            .ToList();
            foreach (var cartItem in cartItems)
            {
                Product product = _context.Products.FirstOrDefault(p => p.ID == cartItem.ProductId);
                mainCarts.Add(new CartItem(product.ID, product.Name, product.Price, cartItem.Size, cartItem.Quantity, product.Image, product.Price * cartItem.Quantity));
            }


            CheckoutViewModel checkout = new CheckoutViewModel
            {
                Carts = mainCarts,
                Address = await _context.DeliveryInfos.FindAsync(user.Id, 1)
            };

            return View(checkout);
        }
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel checkout)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            // Get info cart
            List<CartItem> mainCarts = new List<CartItem>();
            var cartItems = _context.Carts
            .Where(cart => cart.CustomerId == user.Id)
            .ToList();
            foreach (var cartItem in cartItems)
            {
                Product product = _context.Products.FirstOrDefault(p => p.ID == cartItem.ProductId);
                mainCarts.Add(new CartItem(product.ID, product.Name, product.Price, cartItem.Size, cartItem.Quantity, product.Image, product.Price * cartItem.Quantity));
            }
            checkout.Carts = mainCarts;

            Order order = new Order
            {
                CustomerID = user.Id,
                Time_create = DateTime.Now,
                Value = checkout.SubTotal,
                Pay_method = checkout.PaymentMethod,
                Delivery_date = DateTime.Now.AddDays(5),
                Delivery_method = "Express",
                Status = "Pending",
                Note = checkout.Note
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cart in mainCarts)
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderID = order.ID,
                    ProductID = cart.productID,
                    Size = cart.Size,
                    Quantity = cart.Quantity,
                };
                _context.OrderDetails.Add(detail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
