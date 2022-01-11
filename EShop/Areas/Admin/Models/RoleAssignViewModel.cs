using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Models
{
    public class RoleAssignViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool Exist { get; set; }
    }
}
