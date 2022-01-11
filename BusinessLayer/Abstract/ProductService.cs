using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ProductService:GenericService<Product>
    {
        public void ImageUpdate(Product data);
        void RestoreDeleted(Product data);
        public List<Product> OutOfStock();
        public List<Product> RunningOutStock();
    }
}
