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
    public class CategoryManager:CategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void Add(Category p)
        {
            p.Status = true;
            _categoryDal.Add(p);
        }

        public void Delete(Category p)
        {
            var delete = _categoryDal.GetById(p.CategoryId);
            delete.Status = false;
            _categoryDal.Update(delete);
        }

        public void FullDelete(Category data)
        {
            var delete = _categoryDal.GetById(data.CategoryId);
            _categoryDal.FullDelete(delete);

        }

        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public List<Category> GetList(Expression<Func<Category, bool>> filter)
        {
            return _categoryDal.List(filter);
        }

        public List<Category> List()
        {
            return _categoryDal.List();
        }

        public void RestoreDeleted(Category data)
        {
            var restoredeleted = _categoryDal.GetById(data.CategoryId);
            restoredeleted.Status = true;
            _categoryDal.Update(restoredeleted);
        }

        public void Update(Category p)
        {
            var update = _categoryDal.GetById(p.CategoryId);
            update.Name = p.Name;
            _categoryDal.Update(update);
        }
    }
}
