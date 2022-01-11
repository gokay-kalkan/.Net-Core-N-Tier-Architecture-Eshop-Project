using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IFavoriteDal:IRepository<Favorite>
    {
        public Favorite FavoriteGetFind(Expression<Func<Favorite, bool>> filter);
    }
}
