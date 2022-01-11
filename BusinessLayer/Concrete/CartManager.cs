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
    public class CartManager : CartService
    {
        ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }
        public void Add(Cart p)
        {

            _cartDal.Add(p);
        }

        public void Delete(Cart p)
        {
            _cartDal.Delete(p);
        }

        public void DeleteRange(IEnumerable<Cart> p)
        {

            _cartDal.RangeDelete(p);
        }

        public Cart GetById(int id)
        {
            return _cartDal.GetById(id);
        }

        public List<Cart> GetList(Expression<Func<Cart, bool>> filter)
        {
            return _cartDal.List(filter);
        }

        public List<Cart> List()
        {
            return _cartDal.List();
        }

        public void Update(Cart p)
        {
            _cartDal.Update(p);
        }
    }
}
