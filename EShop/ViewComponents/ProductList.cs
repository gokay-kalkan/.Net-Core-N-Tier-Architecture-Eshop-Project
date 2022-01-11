using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewComponents
{
    public class ProductList : ViewComponent
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        ImageManager im = new ImageManager(new EfImageDal());
        public IViewComponentResult Invoke()
        {
            var proudctlist = pm.GetList(x => x.Status == true);
            var imagelist = im.GetList(x => x.Status == true);
            ViewBag.image = imagelist;

            var computerproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Bilgisayar");
            ViewBag.computer = computerproducts;

            var computerimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Bilgisayar");
            ViewBag.computerimage = computerimage;

            var tabletproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Tablet");
            ViewBag.tablet = tabletproducts;

            var tabletimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Tablet");
            ViewBag.tabletimage = tabletimage;

            var phoneproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Telefon");
            ViewBag.phone = phoneproducts;

            var phoneimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Telefon");
            ViewBag.phoneimage = phoneimage;

            var tvproducts = pm.GetList(x => x.Status == true && x.Category.Name == "Televizyon");
            ViewBag.tv = tvproducts;

            var tvimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Televizyon");
            ViewBag.tvimage = tvimage;

            var camera = pm.GetList(x => x.Status == true && x.Category.Name == "Kamera");
            ViewBag.camera = camera;

            var cameraimage = im.GetList(x => x.Status == true && x.Product.Category.Name == "Kamera");
            ViewBag.cameraimage = cameraimage;


            return View(proudctlist);
        }
    }
}
