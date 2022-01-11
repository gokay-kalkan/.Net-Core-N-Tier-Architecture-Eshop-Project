using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
