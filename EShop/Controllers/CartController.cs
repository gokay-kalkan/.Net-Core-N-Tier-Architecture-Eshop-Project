using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        CartManager cm = new CartManager(new EfCartDal());
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var userauthenticationid = HttpContext.Session.GetString("Id");
           

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

        public IActionResult AddCart(int id)
        {
            TempData["Id"] = id;
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return View("LoginCheck");
            }

            var model = HttpContext.Session.GetString("Id");
            var u = db.Products.Find(id);
            var cart = db.Carts.Where(x => x.UserAdminId == model && x.ProductId == id).FirstOrDefault();


            if (cart != null)
            {
                cart.Quantity++;
                cart.Price = u.Price * cart.Quantity;

                db.SaveChanges();
                return RedirectToAction("Index", "Cart");
            }
            var s = new Cart
            {
                UserAdminId = model,
                ProductId = u.ProductId,
                ProductImage = u.Images.Where(x => x.ProductId == u.ProductId).Select(y => y.ImageName).FirstOrDefault(),
                Quantity = 1,
                Price = u.Price,
                Date = DateTime.Now
            };
            cm.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");

        }

        public ActionResult TotalCount(int? count)
        {
            if (User.Identity.IsAuthenticated)
            {

                var model = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
                count = db.Carts.Where(x => x.UserAdminId == model.Id).Count();
                ViewBag.Count = count;
                if (count == 0)
                {
                    ViewBag.Count = "";
                }

                return PartialView();
            }
            return View();
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

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {

                var userid = HttpContext.Session.GetString("Id");

                var sil = db.Carts.Where(x => x.CartId == id && x.UserAdminId == userid).FirstOrDefault();
                if (sil != null)
                {
                    cm.Delete(sil);

                    return RedirectToAction("Index", "Cart");
                }
                else
                {
                    TempData["Error"] = "Yetkisiz İşlem";
                    HttpContext.Session.Remove("Id");
                    return View("LoginCheck");
                }

            }

            return NotFound();
        }

        public ActionResult DeleteRange()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = HttpContext.Session.GetString("Id");


                var sil = db.Carts.Where(x => x.UserAdminId == userid);
                if (sil != null)
                {

                    cm.DeleteRange(sil);

                    return RedirectToAction("Index", "Cart");
                }

                else
                {
                    TempData["Error"] = "Yetkisiz İşlem";
                    HttpContext.Session.Remove("Id");
                    return View("LoginCheck");
                }

            }
            return NotFound();
        }
    }
}
