using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        DataContext db = new DataContext();

        public List<Order> OrderKargoList()
        {
            var orderstate = OrderState.Kargoda;
            var orderstatelist = db.Orders.Where(x => x.OrderState == orderstate).ToList();
            return orderstatelist;
        }

        public List<Order> OrderListState()
        {
            var orderstate = OrderState.Hazırlanıyor;
            var orderstatelist = db.Orders.Where(x => x.OrderState == orderstate).ToList();
            return orderstatelist;
        }

        public Order OrderStateDelete(Order p)
        {
           
            var orderstate = OrderState.Hazırlanıyor;
            var order = db.Orders.Where(x => x.OrderId == p.OrderId && x.OrderState == orderstate).FirstOrDefault();
         
            db.Orders.Remove(order);
            db.SaveChanges();
            return order;
        }
    }
}
