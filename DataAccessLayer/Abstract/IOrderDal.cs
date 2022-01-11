using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IOrderDal:IRepository<Order>
    {
        public List<Order> OrderListState();
        public List<Order> OrderKargoList();

        public Order OrderStateDelete(Order p);

    }
}
