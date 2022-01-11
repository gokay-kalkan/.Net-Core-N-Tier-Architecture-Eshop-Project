using EntityLayer.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori Ad Alanı Boş Geçilemez");
            RuleFor(x => x.Name).MaximumLength(25).WithMessage("Kategori Adı Max 25 karakter alabilir");

        }
    }
}
