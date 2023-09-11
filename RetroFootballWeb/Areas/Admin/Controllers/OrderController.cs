using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;
using System.Data;

namespace RetroFootballWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private DataContext _context;
        public OrderController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Order";

            var orders = await _context.Orders.ToListAsync();
            foreach(Order order in orders)
            {
                order.Customer = await _context.Customers.FindAsync(order.CustomerID);
            }

            return View(orders);
        }
        public async Task<IActionResult> Detail(int orderID)
        {
            var orders = await _context.OrderDetails.Where(o => o.OrderID == orderID).ToListAsync();
            foreach(OrderDetail order in orders)
            {
                order.Product = await _context.Products.FindAsync(order.ProductID);
            }
            return View(orders);
        }
        public async Task<IActionResult> Update(int orderID)
        {
            var order = await _context.Orders.FindAsync(orderID);
            if (order == null) { return NotFound(); }
            if (order.Status == "Pending")
            {
                order.Status = "Confirmed";
                TempData["SuccessMessage"] = "Update order status successfully!";
            }
            else
            if (order.Status == "Confirmed")
            {
                order.Status = "Packing";
                TempData["SuccessMessage"] = "Update order status successfully!";
            }
            else
            if (order.Status == "Packing")
            {
                order.Status = "Shipping";
                TempData["SuccessMessage"] = "Update order status successfully!";
            }
            else
            if (order.Status == "Shipping")
            {
                order.Status = "Delivered";
                TempData["SuccessMessage"] = "Update order status successfully!";
            }
            else
            if (order.Status == "Delivered")
            {
                TempData["SuccessMessage"] = "Please wait customer comfirms the receipt!";
            }
            else
            {
                TempData["SuccessMessage"] = "Order completed!";
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
