using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var list = cm.GetList(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Category data)
        {
            CategoryValidation validationRules = new CategoryValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                cm.Add(data);



                TempData["Success"] = "Kategori Ekleme İşlemi Başarıyla Gerçekleşti";
                return RedirectToAction("Index");

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
            var delete = cm.GetById(id);
            cm.Delete(delete);

            return RedirectToAction("Index");

        }
        public IActionResult DeleteList()
        {
            var deletelist = cm.GetList(x => x.Status == false);

            return View(deletelist);

        }
        public IActionResult RestoreDeleted(int id)
        {
            var delete = cm.GetById(id);
            cm.RestoreDeleted(delete);
            TempData["RestoreDeleted"] = "Silinen Kategori Başarıyla Geri Yüklendi";
            return RedirectToAction("Index");

        }
        public IActionResult FullDelete(int id)
        {
            var delete = cm.GetById(id);
            cm.FullDelete(delete);
            TempData["FullDelete"] = "Kategori Tamamen Silinmiştir.";
            return RedirectToAction("DeleteList");
        }

        public IActionResult Update(int id)
        {
            var update = cm.GetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category data)
        {
            CategoryValidation validationRules = new CategoryValidation();
            ValidationResult result = validationRules.Validate(data);
            if (result.IsValid)
            {
                cm.Update(data);

                TempData["Update"] = "Kategori Güncelleme İşlemi Başarıyla Gerçekleşti";

                return RedirectToAction("Index");

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

    }
}
