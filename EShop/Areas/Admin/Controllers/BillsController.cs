using BusinessLayer.Concrete;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        OrderManager om = new OrderManager(new EfOrderDal());
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var list = om.List();

            var userauthenticationid = HttpContext.Session.GetString("Id");

            var userid = db.UserAdmins.FirstOrDefault(x => x.Id == userauthenticationid);
            if (userid != null)
            {
                var Total = db.Orders.Sum(x => x.ProductPrice * x.Quantity);

                Total = Math.Round(Total);
                ViewBag.Total = "Toplam Tutar =" + Total + "TL";
            }
            return View(list);
        }
    }
}
