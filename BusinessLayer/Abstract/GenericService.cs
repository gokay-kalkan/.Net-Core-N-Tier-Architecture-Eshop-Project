using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface GenericService<T>
    {
        List<T> List();
        void Add(T p);
        T GetById(int id);
        void Update(T p);
        void Delete(T p);
        List<T> GetList(Expression<Func<T, bool>> filter);
    }
}
