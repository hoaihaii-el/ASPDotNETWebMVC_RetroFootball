using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RetroFootballWeb.Models.ViewModels;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;

namespace RetroFootballWeb.Controllers
{
    public class AccountController : Controller
    {
        // Register service manage user and signin
        private UserManager<AppUser> _userManage;
        private SignInManager<AppUser> _signInManager;
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManage)
        {
            _signInManager = signInManager;
            _userManage = userManage;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewData["Active"] = "Login";

            // Create default admin account
            string email = "admin@admin.com";
            string password = "admin123";

            if (await _userManage.FindByEmailAsync(email) == null)
            {
                var user = new AppUser();

                user.Email = email;
                user.UserName = "admin";

                await _userManage.CreateAsync(user, password);

                await _userManage.AddToRoleAsync(user, "Admin");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager
                    .PasswordSignInAsync(login.UserName, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (await _userManage.IsInRoleAsync(await _userManage.FindByNameAsync(login.UserName), "Admin"))
                    {
                        return Redirect("~/Admin");
                    }
                    else
                        return Redirect("~/Home");
                }
                ModelState.AddModelError("", "Invalid username or password!");
            }
            return View(login);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                IdentityResult result = await _userManage.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Register successfully!";
                    await _userManage.AddToRoleAsync(newUser, "User");
                }
                else
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
