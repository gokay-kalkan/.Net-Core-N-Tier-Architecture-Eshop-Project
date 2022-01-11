using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int Quantity { get; set; }


        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string ProductImage { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string UserAdminId { get; set; }
        public virtual UserAdmin UserAdmin { get; set; }


    }
}
