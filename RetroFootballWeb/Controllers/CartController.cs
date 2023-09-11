using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;
        public CartController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index(string customerID = "user1")
        {
            decimal subTotal = 0;
            List<CartItem> mainCarts = new List<CartItem>();

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
        public async Task<IActionResult> AddToCart(string productID, string customerID, int quantity = 1, string size = "")
        {
            try
            {
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
        public async Task<IActionResult> Increase(string customerID, string productID, string size)
        {
            var cart = await _context.Carts.FindAsync(customerID, productID, size);
            if (cart != null)
            {
                cart.Quantity++;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrease(string customerID, string productID, string size)
        {
            var cart = await _context.Carts.FindAsync(customerID, productID, size);
            if (cart != null)
            {
                cart.Quantity--;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(string customerID, string productID, string size)
        {
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
