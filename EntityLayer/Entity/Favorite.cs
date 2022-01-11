using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        public string UserAdminId { get; set; }
        public virtual UserAdmin UserAdmin { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
