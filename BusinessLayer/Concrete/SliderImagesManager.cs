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
    public class SliderImagesManager : SliderImageService
    {
        ISliderImageDal _sliderImageDal;

        public SliderImagesManager(ISliderImageDal sliderDal)
        {
            _sliderImageDal = sliderDal;
        }
        public void Add(SliderImages p)
        {
            _sliderImageDal.Add(p);
        }

        public void Delete(SliderImages p)
        {
            _sliderImageDal.Delete(p);

        }

        public SliderImages GetById(int id)
        {
            return _sliderImageDal.GetById(id);
        }

        public List<SliderImages> GetList(Expression<Func<SliderImages, bool>> filter)
        {
            return _sliderImageDal.List(filter);
        }

        public List<SliderImages> List()
        {
            return _sliderImageDal.List();
        }

        public void Update(SliderImages p)
        {
            _sliderImageDal.Update(p);
        }
    }
}
