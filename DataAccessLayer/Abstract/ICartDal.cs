using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICartDal:IRepository<Cart>
    {
        void RangeDelete(IEnumerable<Cart> p);
    }
}
