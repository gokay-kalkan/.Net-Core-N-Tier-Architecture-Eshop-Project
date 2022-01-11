using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entity
{
   public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Boş Geçemezsiniz")]
        public string CommentText { get; set; }
        public bool Status { get; set; }
        public int RatingProduct { get; set; }

        public DateTime CommentDate { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string UserAdminId { get; set; }
        public virtual UserAdmin UserAdmin { get; set; }
    }
}
