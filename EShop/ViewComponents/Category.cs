using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.ViewComponents
{
    public class Category : ViewComponent
    {

        public IViewComponentResult Invoke()
        {

            CategoryManager cm = new CategoryManager(new EfCategoryDal());

            var list = cm.GetList(x => x.Status == true);

            return View(list);

        }
    }
}
