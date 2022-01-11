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
    public class ImageManager : ImageService
    {
        IImageDal _iimageDal;

        public ImageManager(IImageDal iimageDal)
        {
            _iimageDal = iimageDal;
        }

        public void Add(Image p)
        {
            _iimageDal.Add(p);
        }

        public void Delete(Image p)
        {
            _iimageDal.Delete(p);
        }

        public Image GetById(int id)
        {
            return _iimageDal.GetById(id);
        }

        public List<Image> GetList(Expression<Func<Image, bool>> filter)
        {
            return _iimageDal.List(filter);
        }

        public List<Image> List()
        {
            return _iimageDal.List();
        }

        public void Update(Image p)
        {
            var update = _iimageDal.GetById(p.ImageId);
            update.ImageName = p.File.FileName;
            _iimageDal.Update(update);
        }
    }
}
