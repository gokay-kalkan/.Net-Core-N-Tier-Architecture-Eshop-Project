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
    public class GeneralSettingsManager : GeneralSettingService
    {
        IGeneralSettingsDal _generalSettingsDal;
        public GeneralSettingsManager(IGeneralSettingsDal generalSettingsDal)
        {
            _generalSettingsDal = generalSettingsDal;
        }
        public void Add(GeneralSetting p)
        {
            _generalSettingsDal.Add(p);
        }

        public void Delete(GeneralSetting p)
        {
            _generalSettingsDal.Delete(p);
        }

        public GeneralSetting GetById(int id)
        {
            return _generalSettingsDal.GetById(id);
        }

        public List<GeneralSetting> GetList(Expression<Func<GeneralSetting, bool>> filter)
        {
            return _generalSettingsDal.List(filter);
        }

        public List<GeneralSetting> List()
        {
            return _generalSettingsDal.List();
        }

        public void Update(GeneralSetting p)
        {
            _generalSettingsDal.Update(p);
        }
    }
}
