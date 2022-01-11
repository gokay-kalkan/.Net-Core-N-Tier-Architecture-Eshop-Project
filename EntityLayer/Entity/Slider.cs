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
    public class Slider
    {
        public Slider()
        {
            SliderImages = new List<SliderImages>();
        }
        [Key]
        public int SliderId { get; set; }
        [NotMapped]
        public IEnumerable<IFormFile> SliderImage { get; set; }
        public virtual List<SliderImages> SliderImages { get; set; }
    }
}
