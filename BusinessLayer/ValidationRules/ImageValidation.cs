using EntityLayer.Entity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ImageValidation : AbstractValidator<Image>
    {

        public ImageValidation()
        {
            RuleFor(x => x.File).NotEmpty().WithMessage("Resim Alanı Boş Geçilemez");

        }

    }
}
