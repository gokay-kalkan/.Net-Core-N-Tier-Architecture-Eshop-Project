using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private DataContext c = new DataContext();
        DbSet<T> _object;
        public GenericRepository()
        {

            _object = c.Set<T>();
        }
        public void Add(T p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public void Delete(T p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            return _object.Find(id);
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            _object.Update(p);
            c.SaveChanges();
        }
    }
}
