using EntityLayer.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Boş Geçilemez");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Max 50 Karakter Uzunluğunda Olabilir");
            RuleFor(x => x.Name).MaximumLength(300).WithMessage("Max 300 Karakter Uzunluğunda Olabilir");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün Fiyat Alanı Boş Geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün Açıklama Alanı Boş Geçilemez");

            RuleFor(x => x.Stock).NotEmpty().WithMessage("Ürün Stok Alanı Boş Geçilemez");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori Alanı Boş Geçilemez");

            RuleFor(x => x.Image).NotEmpty().WithMessage("Ürün Resimleri Boş Geçilemez");
        }
    }
}
