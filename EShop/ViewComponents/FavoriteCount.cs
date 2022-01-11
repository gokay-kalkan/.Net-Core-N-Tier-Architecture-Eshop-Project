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
    public class FavoriteCount : ViewComponent
    {
        FavoriteManager fm = new FavoriteManager(new EfFavoriteDal());
        public IViewComponentResult Invoke()
        {
            var user = HttpContext.Session.GetString("Id");
            var count = fm.GetList(x => x.UserAdminId == user).Count();
            ViewBag.count = count;
            return View();
        }
    }
}
