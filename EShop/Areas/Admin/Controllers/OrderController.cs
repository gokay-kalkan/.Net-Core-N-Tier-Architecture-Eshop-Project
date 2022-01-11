using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        OrderManager om = new OrderManager(new EfOrderDal());
        public IActionResult Index()
        {
            
            var orderlist = om.List();

            return View(orderlist);
        }

        public IActionResult PendingOrders()
        {
            var orders = om.OrderStateList();
       
            return View(orders);
        }

        public IActionResult PendingOrdersKargo()
        {
            var orders = om.OrderKargoList();
        
            return View(orders);
        }

        public PartialViewResult OrderPendingCount()
        {
            var count = om.OrderStateList().Count();
            ViewBag.count = count;
            return PartialView(count);
        }
        public IActionResult OrderDetail(int id)
        {
            var order = om.GetById(id);
            TempData["Id"] = id;
            TempData["order"] = order;
            return View(order);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult OrderStateUpdate(int id, Order data)
        {
            TempData["Id"] = id;
            var order = om.GetById(id);
            order.OrderState = data.OrderState;
            om.Update(order);
        
            
            return RedirectToAction("Index");
        }
    }
}
