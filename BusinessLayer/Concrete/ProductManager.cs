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
    public class ProductManager:ProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product p)
        {
            p.Status = true;
            _productDal.Add(p);
        }

        public void Delete(Product p)
        {
            p.Status = false;
            _productDal.Delete(p);
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public List<Product> GetList(Expression<Func<Product, bool>> filter)
        {
            return _productDal.List(filter);
        }

        public void ImageUpdate(Product data)
        {
            var update = _productDal.GetById(data.ProductId);
            update.Image = data.Image;
            _productDal.Update(update);
        }

        public List<Product> List()
        {
            return _productDal.List();
        }

        public List<Product> OutOfStock()
        {
            return _productDal.OutOfStock();
        }

        public void RestoreDeleted(Product data)
        {
            var restoredeleted = _productDal.GetById(data.ProductId);
            restoredeleted.Status = true;
            _productDal.Update(restoredeleted);
        }

        public List<Product> RunningOutStock()
        {
            return _productDal.RunningOutStock();
        }

        public void Update(Product p)
        {
            var update = _productDal.GetById(p.ProductId);
            update.Name = p.Name;
            update.Description = p.Description;
            update.Popular = p.Popular;
            update.Approved = p.Approved;
            update.Stock = p.Stock;
            update.Price = p.Price;
            update.CategoryId = p.CategoryId;
            _productDal.Update(update);
        }
    }
}
