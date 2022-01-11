using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.Areas.ViewComponents
{
    public class OrderStateKargo : ViewComponent
    {
        OrderManager om = new OrderManager(new EfOrderDal());
        public IViewComponentResult Invoke()
        {
            var count = om.OrderKargoList().Count();
            ViewBag.count = count;
            return View(count);
        }
    }
}
