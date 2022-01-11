using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface OrderService:GenericService<Order>
    {
        public List<Order> OrderStateList();
        public List<Order> OrderKargoList();
        public Order OrderStateDelete(Order p);
    }
}
