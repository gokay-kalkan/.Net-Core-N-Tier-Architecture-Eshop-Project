using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager:CommentService
    {
        ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public void Add(Comment p)
        {
            p.CommentDate = DateTime.Now;
            p.Status = true;
            _commentDal.Add(p);
        }

        public void CommentDelete(int id)
        {
            var comment = _commentDal.GetById(id);
            _commentDal.Delete(comment);
        }

        public void Delete(Comment p)
        {
            var delete = _commentDal.GetById(p.CommentId);
            delete.Status = false;
            _commentDal.Update(delete);
        }

        public Comment GetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> GetList(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.List(filter);
        }

        public List<Comment> List()
        {
            return _commentDal.List();
        }

        public void Update(Comment p)
        {
            _commentDal.Update(p);
        }
    }
}
