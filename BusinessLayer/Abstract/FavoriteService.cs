using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface FavoriteService:GenericService<Favorite>
    {
        public Favorite FavoriteFind(Expression<Func<Favorite, bool>> filter);
    }
}
