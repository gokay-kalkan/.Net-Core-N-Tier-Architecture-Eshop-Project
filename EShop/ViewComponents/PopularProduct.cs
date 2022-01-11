using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewComponents
{
    public class PopularProduct : ViewComponent
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        ImageManager im = new ImageManager(new EfImageDal());
        public IViewComponentResult Invoke()
        {
            var popularproducts = pm.GetList(x => x.Status == true && x.Popular == true);

            var popularimage = im.GetList(x => x.Status == true);
            ViewBag.popularimage = popularimage;

            return View(popularproducts);
        }
    }
}
