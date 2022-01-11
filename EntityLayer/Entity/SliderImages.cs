using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class SliderImages
    {
        [Key]
        public int SliderImagesId { get; set; }
        public string SliderName { get; set; }

        [NotMapped]
        public IFormFile SliderImageName { get; set; }
        public int SliderId { get; set; }
        public virtual Slider Slider { get; set; }
    }
}
