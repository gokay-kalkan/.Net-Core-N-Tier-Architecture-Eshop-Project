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
    public class EfFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        DataContext db = new DataContext();

        public Favorite FavoriteGetFind(Expression<Func<Favorite, bool>> filter)
        {
            var favorite = db.Favorites.Where(filter).FirstOrDefault();
            return favorite;
        }
    }
}
