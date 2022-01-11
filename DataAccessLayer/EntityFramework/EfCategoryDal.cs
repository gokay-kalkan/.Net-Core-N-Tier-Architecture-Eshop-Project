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
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        DataContext db = new DataContext();
        public void FullDelete(Category p)
        {
            var delete = db.Categories.Find(p.CategoryId);
            db.Categories.Remove(delete);
        }
    }
}
