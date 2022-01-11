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
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        DataContext db = new DataContext();
        public List<Product> OutOfStock()
        {
            return db.Products.Where(x => x.Stock == 0).ToList();
        }

        public List<Product> RunningOutStock()
        {
            return db.Products.Where(x => x.Stock < 50).ToList();
        }
    }
}
