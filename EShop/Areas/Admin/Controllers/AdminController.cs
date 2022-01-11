using EntityLayer.Entity;
using EShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", " Hatalı lütfen bilgilerinizi kontrol ediniz");

            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlendi");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Username", user.UserName);
                HttpContext.Session.SetString("Id", user.Id);
                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("Index");
            }
            else
            {
                await _userManager.AccessFailedAsync(user);

                int fail = await _userManager.GetAccessFailedCountAsync(user);
                if (fail == 3)
                {
                    await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(2)));
                    ModelState.AddModelError("", "Hesabınız 3 başarısız girişten dolayı 2 dakika süreyle kilitlenmiştir. Daha sonra tekrar deneyiniz");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz eposta veya şifre");
                }
            }
            return View();
        }
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Login");
        }
    }
}
