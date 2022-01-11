using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        public List<Product> OutOfStock();
        public List<Product> RunningOutStock();
    }
}
