using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;
using System.Data;

namespace RetroFootballWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;
        public CustomerController(UserManager<AppUser> userManager, DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Customer";
            return View(await _context.Customers.ToListAsync()) ;
        }
    }
}
