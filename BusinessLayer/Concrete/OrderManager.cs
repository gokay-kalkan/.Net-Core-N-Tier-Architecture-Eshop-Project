using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderManager : OrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Add(Order p)
        {
            _orderDal.Add(p);
        }

        public void Delete(Order p)
        {
            _orderDal.Delete(p);
        }

        public Order GetById(int id)
        {
            return _orderDal.GetById(id);
        }

        public List<Order> GetList(Expression<Func<Order, bool>> filter)
        {
            return _orderDal.List(filter);
        }

        public List<Order> List()
        {
            return _orderDal.List();
        }

        public List<Order> OrderKargoList()
        {
            return _orderDal.OrderKargoList();
        }

        public Order OrderStateDelete(Order p)
        {
            
            return _orderDal.OrderStateDelete(p);
        }

        public List<Order> OrderStateList()
        {
            return _orderDal.OrderListState();
        }


        public void Update(Order p)
        {
            _orderDal.Update(p);
        }
    }
}
