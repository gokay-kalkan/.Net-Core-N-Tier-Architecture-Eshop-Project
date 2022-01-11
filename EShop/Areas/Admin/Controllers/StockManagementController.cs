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
    public class StockManagementController : Controller
    {
        ProductManager pm = new ProductManager(new EfProductDal());
        public IActionResult OutOfStock()
        {
            var outofstock = pm.OutOfStock();
            return View(outofstock);
        }

        public IActionResult RunningOutStock()
        {
            var runningoutstock = pm.RunningOutStock();
            return View(runningoutstock);
        }
    }
}
