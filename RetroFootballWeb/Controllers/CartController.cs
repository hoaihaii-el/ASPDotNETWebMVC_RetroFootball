using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;
        private UserManager<AppUser> _userManager;
        public CartController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            decimal subTotal = 0;
            List<CartItem> mainCarts = new List<CartItem>();

            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var cartItems = _context.Carts
            .Where(cart => cart.CustomerId == customerID)
            .ToList();
            foreach (var cartItem in cartItems)
            {
                Product product = _context.Products.FirstOrDefault(p => p.ID == cartItem.ProductId);
                subTotal += product.Price * cartItem.Quantity;
                mainCarts.Add(new CartItem(product.ID, product.Name, product.Price, cartItem.Size, cartItem.Quantity, product.Image, product.Price * cartItem.Quantity));
            }
            ViewData["SubTotalCart"] = subTotal;
            ViewData["Shipping"] = 10;
            ViewData["Total"] = subTotal + 10;
            return View(mainCarts);
        }
        public async Task<IActionResult> AddToCart(string productID, int quantity = 1, string size = "")
        {
            try
            {
                // Get info user authenticated
                AppUser user = await _userManager.GetUserAsync(HttpContext.User);
                string customerID = user.Id;

                var product = await _context.Products.FindAsync(productID);
                if (String.IsNullOrEmpty(size))
                {
                    if (product.Size_s > 0) size = "S";
                    else
                    if (product.Size_m > 0) size = "M";
                    else
                    if (product.Size_l > 0) size = "L";
                    else
                        size = "XL";
                }
                var item = await _context.Carts.FindAsync(customerID, productID, size);
                if (item != null)
                {
                    item.Quantity += quantity;
                }
                else
                {
                    var cartItem = new Cart
                    {
                        ProductId = productID,
                        CustomerId = customerID,
                        Quantity = quantity,
                        Size = size
                    };
                    _context.Carts.Add(cartItem);
                }
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Add to cart successfully!";
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Increase(string productID, string size)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var cart = await _context.Carts.FindAsync(customerID, productID, size);
            if (cart != null)
            {
                cart.Quantity++;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrease(string productID, string size)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var cart = await _context.Carts.FindAsync(customerID, productID, size);
            if (cart != null)
            {
                if (cart.Quantity <= 1) return RedirectToAction("Index");
                cart.Quantity--;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(string productID, string size)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            string customerID = user.Id;

            var cart = await _context.Carts.FindAsync(customerID, productID, size);
            if (cart != null)
            {
                _context.Carts.Remove(cart);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
