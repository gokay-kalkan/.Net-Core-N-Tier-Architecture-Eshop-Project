using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCartDal : GenericRepository<Cart>, ICartDal
    {
        DataContext c = new DataContext();
        public void RangeDelete(IEnumerable<Cart> p)
        {
            c.Carts.RemoveRange(p);
            c.SaveChanges();
        }
    }
}
