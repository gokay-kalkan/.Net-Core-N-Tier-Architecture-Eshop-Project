using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewComponents
{
    public class Slider : ViewComponent
    {
        SliderManager sliderManager = new SliderManager(new EfSliderDal());
        DataContext db = new DataContext();
        public IViewComponentResult Invoke()
        {
            var list = db.SliderImages.ToList();


            return View(list);
        }
    }
}
