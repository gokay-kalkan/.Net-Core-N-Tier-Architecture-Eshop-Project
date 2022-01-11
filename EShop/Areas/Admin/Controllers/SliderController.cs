using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        SliderManager sm = new SliderManager(new EfSliderDal());
        SliderImagesManager sim = new SliderImagesManager(new EfSliderImagesDal());

        IWebHostEnvironment env;


        public SliderController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        public IActionResult Index()
        {
            return View(sm.List());
        }

        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Slider data)
        {
            if (data.SliderImage != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                foreach (var item in data.SliderImage)
                {

                    var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        item.CopyTo(dosyaAkisi);
                    }

                    data.SliderImages.Add(new SliderImages { SliderName = item.FileName });

                }
                sm.Add(data);
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult SliderList(int id)
        {
            var update = sim.GetList(x => x.SliderId == id);
            return View(update);
        }
        public IActionResult Update(int id)
        {
            var update = sim.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SliderImages data)
        {
            if (data.SliderName != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");


                var tamDosyaAdi = Path.Combine(dosyaYolu, data.SliderImageName.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {


                    data.SliderImageName.CopyTo(dosyaAkisi);
                }


                sim.Update(data);
                return RedirectToAction("Index");


            }
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var delete = sim.GetById(id);
            sim.Delete(delete);
            return RedirectToAction("Index");
        }
    }
}
