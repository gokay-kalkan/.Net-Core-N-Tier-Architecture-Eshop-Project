using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Rol Adı Boş Geçilemez")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
