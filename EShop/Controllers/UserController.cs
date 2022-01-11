using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using EShop.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {

        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        OrderManager om = new OrderManager(new EfOrderDal());
        CartManager cm = new CartManager(new EfCartDal());
        FavoriteManager fm = new FavoriteManager(new EfFavoriteDal());
        DataContext db = new DataContext();
        public UserController(UserManager<UserAdmin> userManager, SignInManager<UserAdmin> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        
        public IActionResult OrderList()
        {
            var user = HttpContext.Session.GetString("Id");
            var orderlist = om.GetList(x => x.UserAdminId == user);
            if (user == null)
            {
                ViewBag.info = "Öncelikle Sisteme Giriş Yapınız";
            }
            return View(orderlist);
        }

        [AllowAnonymous]
        public IActionResult OrderCancellation(int id)
        {

            var delete = om.GetById(id);
           
          om.OrderStateDelete(delete);

            return RedirectToAction("OrderList");
        }

        public IActionResult FavoriteProducts()
        {
            var user = HttpContext.Session.GetString("Id");
            var favoritelist = fm.GetList(x => x.UserAdminId == user);
            return View(favoritelist);
        }
        public IActionResult UserCartList()
        {
            var userauthenticationid = HttpContext.Session.GetString("Id");
            var username = User.Identity.Name;
            var user = db.UserAdmins.FirstOrDefault(x => x.Email == username);

            var model = cm.GetList(x => x.UserAdminId == userauthenticationid);
            var userid = db.UserAdmins.FirstOrDefault(x => x.Id == userauthenticationid);
            if (userauthenticationid == null)
            {
                return View("LoginCheck");
            }

            if (model != null)
            {
                if (userid == null)
                {
                    ViewBag.Total = "Sepetinizde ürün bulunmamaktadır";
                }

                else if (userid != null)
                {
                    var Total = db.Carts.Where(x => x.UserAdminId == userid.Id).Sum(x => x.Product.Price * x.Quantity);

                    Total = Math.Round(Total, 0);
                    ViewBag.Total = "Toplam Tutar =" + Total + "TL";
                }
                ViewBag.id = TempData["Id"];
                return View(model);

            }
            return NotFound();
        }

        public ActionResult increase(int id)
        {
            var model = cm.GetById(id);
            model.Quantity++;
            model.Price = model.Price * model.Quantity;
            cm.Update(model);

            return RedirectToAction("Index", "Cart");
        }
        public ActionResult toreduce(int id)
        {
            var model = cm.GetById(id);
            if (model.Quantity == 1)
            {
                cm.Delete(model);

            }
            model.Quantity--;
            model.Price = model.Price * model.Quantity;
            cm.Update(model);
            return RedirectToAction("Index", "Cart");
        }

        public void DynamicAmount(int id, int miktari)
        {
            var model = cm.GetById(id);
            model.Quantity = miktari;
            model.Price = model.Price * model.Quantity;
            cm.Update(model);

        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
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
                return RedirectToAction("Index", "Home");
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
        [AllowAnonymous]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            HttpContext.Session.Remove("Id");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            UserAdmin user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            RegisterModel userViewModel = user.Adapt<RegisterModel>();
            return View(userViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(RegisterModel userModel)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");

            if (ModelState.IsValid)
            {

                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = userModel.UserName;
                user.Email = userModel.Email;
                user.FullName = userModel.FullName;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                    ViewBag.success = "Bilgileriniz Başarıyla Güncellenmiştir";


                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", "Bir Hata Oluştu");
                    }
                }
            }

            return View(userModel);
        }

    }
}
