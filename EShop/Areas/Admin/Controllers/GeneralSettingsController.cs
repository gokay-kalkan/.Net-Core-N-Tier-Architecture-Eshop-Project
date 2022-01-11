using BusinessLayer.Concrete;
using DataAccessLayer.Data;
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
    public class GeneralSettingsController : Controller
    {
        GeneralSettingsManager gsm = new GeneralSettingsManager(new EfGeneralSettingsDal());
        DataContext db = new DataContext();
        IWebHostEnvironment env;


        public GeneralSettingsController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        public IActionResult Index()
        {
            var list = gsm.List();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(GeneralSetting data)
        {

            var dosyaYolu = Path.Combine(env.WebRootPath, "img");

            if (data.Logo == null)
            {

                gsm.Add(data);
                return RedirectToAction("Index");
            }
            else
            {
                var tamDosyaAdilogo = Path.Combine(dosyaYolu, data.Logo.FileName);

                using (var dosyaAkisi = new FileStream(tamDosyaAdilogo, FileMode.Create))
                {

                    data.LogoName = data.Logo.FileName;
                    data.Logo.CopyTo(dosyaAkisi);
                }
                gsm.Add(data);
                return RedirectToAction("Index");
            }


        }
        public IActionResult Update(int id)
        {
            var update = gsm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(GeneralSetting data)
        {
            if (data != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");


                var tamDosyaAdilogo = Path.Combine(dosyaYolu, data.Logo.FileName);

                using (var dosyaAkisi = new FileStream(tamDosyaAdilogo, FileMode.Create))
                {

                    data.LogoName = data.Logo.FileName;
                    data.Logo.CopyTo(dosyaAkisi);
                }

            }
            gsm.Update(data);
            return RedirectToAction("Index");

        }
    }
}
