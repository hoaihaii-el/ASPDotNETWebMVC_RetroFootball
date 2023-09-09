using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Customer";
            return View(await _context.Customers.ToListAsync()) ;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Add new customer successfully!";

                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Edit(string customerID)
        {
            if (customerID == null) return NotFound();
            var customer = await _context.Customers.FindAsync(customerID);

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Edit customer successfully!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string customerID)
        {
            if (customerID == null) return NotFound();
            var customer = await _context.Customers.FindAsync(customerID);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Delete customer successfully!";
            return RedirectToAction("Index");
        }
    }
}
