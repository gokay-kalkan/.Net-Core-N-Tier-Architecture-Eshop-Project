using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.ViewComponents
{
    public class OutOfStock:ViewComponent
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        public IViewComponentResult Invoke()
        {
            var count = pm.OutOfStock().Count();
            ViewBag.count = count;

            return View();
        }
    }
}
