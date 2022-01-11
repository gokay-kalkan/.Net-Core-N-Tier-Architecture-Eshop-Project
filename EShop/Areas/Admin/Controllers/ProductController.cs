using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        IWebHostEnvironment env;


        public ProductController(IWebHostEnvironment _env)
        {
            env = _env;
        }
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        ImageManager im = new ImageManager(new EfImageDal());
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var list = pm.List();
            return View(list);
        }

        public IActionResult Create()
        {
            DropDown();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product data)
        {
            DropDown();
            ProductValidation validationRules = new ProductValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                    foreach (var item in data.Image)
                    {

                        var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                        using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                        {


                            item.CopyTo(dosyaAkisi);
                        }

                        data.Images.Add(new Image { ImageName = item.FileName, Status = true });

                    }
                    pm.Add(data);

                    TempData["Success"] = "Ekleme İşlemi Başarıyla Gerçekleşti";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }


            return View();
        }
        public IActionResult Delete(int id)
        {
            var delete = pm.GetById(id);
            pm.Delete(delete);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteList()
        {
            var deletelist = pm.GetList(x => x.Status == false);

            return View(deletelist);


        }
        public IActionResult RestoreDeleted(int id)
        {
            var delete = pm.GetById(id);
            pm.RestoreDeleted(delete);
            TempData["RestoreDeleted"] = "Silinen Kategori Başarıyla Geri Yüklendi";
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            DropDown();
            var update = pm.GetById(id);
            return View(update);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product data)
        {
            DropDown();
            var update = pm.GetById(data.ProductId);

            if (update.Image != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                foreach (var item in data.Image)
                {

                    var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        item.CopyTo(dosyaAkisi);
                    }

                    data.Images.Add(new Image { ImageName = item.FileName, Status = true });

                }
                pm.ImageUpdate(data);
                TempData["Update"] = "Güncelleme İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }
            else
            {
                pm.Update(data);
                TempData["Update"] = "Güncelleme İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction("Index");
            }



        }
        public IActionResult ImageList(int id)
        {
            var imagelist = im.GetList(x => x.Status == true && x.ProductId == id);
            return View(imagelist);
        }

        public IActionResult ImageCreate(int id)
        {
            var image = pm.GetById(id);
            return View(image);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageCreate(Product data)
        {
            var image = pm.GetById(data.ProductId);
            if (data.Image != null)
            {
                var dosyaYolu = Path.Combine(env.WebRootPath, "img");
                foreach (var item in data.Image)
                {


                    var tamDosyaAdi = Path.Combine(dosyaYolu, item.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        item.CopyTo(dosyaAkisi);
                    }

                    im.Add(new Image { ImageName = item.FileName, Status = true, ProductId = image.ProductId });


                }


                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult ImageDelete(int id)
        {
            var delete = im.GetById(id);
            im.Delete(delete);
            return RedirectToAction("Index");
        }

        public IActionResult ImageUpdate(int id)
        {
            var update = im.GetById(id);

            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageUpdate(Image data)
        {
            ImageValidation validationRules = new ImageValidation();
            ValidationResult validationResult = validationRules.Validate(data);

            if (validationResult.IsValid)
            {
                if (data.File != null)
                {
                    var dosyaYolu = Path.Combine(env.WebRootPath, "img");

                    var tamDosyaAdi = Path.Combine(dosyaYolu, data.File.FileName);
                    using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                    {


                        data.File.CopyTo(dosyaAkisi);
                    }

                    im.Update(data);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }



            return View();
        }
        public void DropDown()
        {
            List<SelectListItem> value1 = (from i in cm.GetList(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.CategoryId.ToString()
                                           }).ToList();
            ViewBag.category = value1;
        }


    }
}
