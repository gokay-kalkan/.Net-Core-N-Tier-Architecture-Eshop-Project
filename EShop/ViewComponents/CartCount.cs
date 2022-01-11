using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewComponents
{
    public class CartCount : ViewComponent
    {
        CartManager cm = new CartManager(new EfCartDal());
        public IViewComponentResult Invoke()
        {
            var user = HttpContext.Session.GetString("Id");
            var cartcount = cm.GetList(x => x.UserAdminId == user).Count();
            ViewBag.cartcount = cartcount;
            return View();
        }
    }
}
