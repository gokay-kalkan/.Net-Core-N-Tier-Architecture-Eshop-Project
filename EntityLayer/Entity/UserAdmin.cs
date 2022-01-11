using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
    public class UserAdmin : IdentityUser
    {

        public string FullName { get; set; }
        public string Addres { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public bool Status { get; set; }
        public virtual List<Cart> Carts { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
      
    }
}
