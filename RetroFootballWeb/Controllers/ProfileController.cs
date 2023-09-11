using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;
using System.Reflection.Metadata.Ecma335;

namespace RetroFootballWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DataContext _context;
        private UserManager<AppUser> _userManager;
        public ProfileController(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Profile";

            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            Customer customer = await _context.Customers.FindAsync(user.Id);

            return View(customer);
        }
        public async Task<IActionResult> DetailAddress()
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            return View(await _context.DeliveryInfos.Where(a => a.CustomerID == user.Id).OrderBy(a => a.Priority).ToListAsync());
        }
        public async Task<IActionResult> EditProfile(string customerID)
        {
            var customer = await _context.Customers.FindAsync(customerID);

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Edit your profile successfully!";
            }

            return RedirectToAction("Index");
        }
        public IActionResult NewAddress()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAddress(DeliveryInfo info)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            info.CustomerID = user.Id;
            info.Priority = 1;
            info.Priority = await _context.DeliveryInfos.Take(1)
                .Where(i => i.CustomerID == info.CustomerID)
                .Select(i => i.Priority).FirstOrDefaultAsync() + 1;
            if (ModelState.IsValid)
            {
                _context.DeliveryInfos.Add(info);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Add successfully!";
            }

            return RedirectToAction("DetailAddress");
        }
        public async Task<IActionResult> EditAddress(string customerID, int priority)
        {
            var info = await _context.DeliveryInfos.FindAsync(customerID, priority);

            return View(info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress(DeliveryInfo info)
        {
            // Get info user authenticated
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            info.CustomerID = user.Id;
            info.Priority = 1;
            info.Priority = await _context.DeliveryInfos.Take(1)
                .Where(i => i.CustomerID == info.CustomerID)
                .Select(i => i.Priority).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                _context.DeliveryInfos.Update(info);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Edit successfully!";
            }

            return RedirectToAction("DetailAddress");
        }
        public async Task<IActionResult> DeleteAddress(string customerID, int priority)
        {
            var info = await _context.DeliveryInfos.FindAsync(customerID, priority);

            _context.DeliveryInfos.Remove(info);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Delete successfully!";

            return RedirectToAction("DetailAddress");
        }
        public async Task<IActionResult> SetAddressDefault(string customerID, int priority)
        {
            var def = await _context.DeliveryInfos.FindAsync(customerID, 1);

            var info = await _context.DeliveryInfos.FindAsync(customerID, priority);

            int temp = def.Priority;
            def.Priority = info.Priority;
            info.Priority = temp;

            _context.DeliveryInfos.Update(info);
            _context.DeliveryInfos.Update(def);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Set as default successfully!";

            return RedirectToAction("DetailAddress");
        }
    }
}
