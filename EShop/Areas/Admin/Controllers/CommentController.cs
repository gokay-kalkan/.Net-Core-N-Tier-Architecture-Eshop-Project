using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        CommentManager cm = new CommentManager(new EfCommentDal());
        public IActionResult Index()
        {
            var list = cm.GetList(x => x.Status == true);
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            var delete = cm.GetById(id);
            cm.Delete(delete);
            return RedirectToAction("Index");
        }


    }
}
