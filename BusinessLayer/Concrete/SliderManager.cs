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
    public class SliderManager : SliderService
    {
        ISliderDal _sliderDal;
        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }
        public void Add(Slider p)
        {
            _sliderDal.Add(p);
        }

        public void Delete(Slider p)
        {
            _sliderDal.Delete(p);
        }

        public Slider GetById(int id)
        {
            return _sliderDal.GetById(id);
        }

        public List<Slider> GetList(Expression<Func<Slider, bool>> filter)
        {
            return _sliderDal.List(filter);
        }

        public List<Slider> List()
        {
            return _sliderDal.List();
        }

        public void Update(Slider p)
        {
            _sliderDal.Update(p);
        }
    }
}
