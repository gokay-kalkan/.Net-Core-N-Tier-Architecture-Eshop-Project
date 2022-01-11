using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class ChangePasswordModel
    {
      
            [Required(ErrorMessage = "Eski Şifreniz Gereklidir.")]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }
            [Required(ErrorMessage = "Yeni Şifreniz Gereklidir.")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Yeni Şifre Tekrar Girmeniz Gereklidir.")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Yeni Şifreniz Alanı İle Aynı Olmak Zorunda.")]
            public string ConfirmPassword { get; set; }
        
    }
}
