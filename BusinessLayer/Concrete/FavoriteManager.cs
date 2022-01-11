using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FavoriteManager : FavoriteService
    {
        IFavoriteDal _favoriteDal;
        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }
        public void Add(Favorite p)
        {
            _favoriteDal.Add(p);
        }

        public void Delete(Favorite p)
        {
            _favoriteDal.Delete(p);
        }

        public Favorite FavoriteFind(Expression<Func<Favorite, bool>> filter)
        {
            return _favoriteDal.FavoriteGetFind(filter);
        }

        public Favorite GetById(int id)
        {
            return _favoriteDal.GetById(id);
        }

        public List<Favorite> GetList(Expression<Func<Favorite, bool>> filter)
        {
            return _favoriteDal.List(filter);
        }

        public List<Favorite> List()
        {
            return _favoriteDal.List();
        }

        public void Update(Favorite p)
        {
            _favoriteDal.Update(p);
        }
    }
}
