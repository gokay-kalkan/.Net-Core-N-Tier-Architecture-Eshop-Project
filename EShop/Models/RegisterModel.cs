using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class RegisterModel
    {
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Boş Geçilemez")]
        [EmailAddress(ErrorMessage = "Geçersiz Email Adresi")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Boş Geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor")]

        public string RePassword { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Boş Geçilemez")]

        public string UserName { get; set; }

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string FullName { get; set; }
    }
}
