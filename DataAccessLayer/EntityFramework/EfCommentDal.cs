using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Data;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        DataContext db = new DataContext();
        public void CommentDelete(int id)
        {
            var comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
        }
    }
}
