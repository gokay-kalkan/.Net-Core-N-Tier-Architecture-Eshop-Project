using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface CommentService:GenericService<Comment>
    {
        public void CommentDelete(int id);
    }
}
