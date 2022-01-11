using EntityLayer.Entity;
using EShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAdmin user = new UserAdmin
            {
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName

            };

            IdentityRole role = new IdentityRole()
            {
                Name = "User",

            };
            await _roleManager.CreateAsync(role);

            var result = await _userManager.CreateAsync(user, model.Password);
            var resultt = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded || resultt.Succeeded)
            {
                TempData["success"] = "Sitemize Başarıyla Kayıt Oldunuz";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
                return View(model);
            }


        }
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            UserAdmin user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string passwordResetLink = Url.Action("UpdatePassword", "Account", new { userId = user.Id, token = resetToken }, HttpContext.Request.Scheme);

                MailHelper.ResetPassword.PasswordResetSendMail(passwordResetLink);
                ViewBag.State = true;
            }
            else
                ViewBag.State = false;

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdatePassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            //UserAdmin userr = new UserAdmin();
            //UpdatePasswordModel resetpassword = new UpdatePasswordModel() { Email = userr.Email };
            //var check = _userManager.FindByEmailAsync(userr.Email);
            //TempData["check"] = check;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword([Bind("NewPassword")] ResetPasswordModel model)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();
            UserAdmin user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["Success"] = "Şifreniz Başarıyla Güncellenmiştir";

                }
            }
            else
            {

                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı");
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name);

                bool exist = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (exist)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, model.NewPassword, true, false);
                        ViewBag.success = "Şifre Başarılı Bir Şekilde Değişti";
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski Şifreniz Yanlış");
                }


            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
