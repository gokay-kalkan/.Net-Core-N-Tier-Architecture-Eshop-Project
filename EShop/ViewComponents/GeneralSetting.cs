using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.ViewComponents
{
    public class GeneralSetting:ViewComponent
    {
        GeneralSettingsManager gsm = new GeneralSettingsManager(new EfGeneralSettingsDal());
        public IViewComponentResult Invoke()
        {
            var list = gsm.List();
            return View(list);
        }
    }
}
