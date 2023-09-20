using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;

namespace blogpost.Services
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        public CommentService(DataContext context)
        {
            _context = context;
        }

        public bool ExistComment(int commentId)
        {
            return _context.Comments_dbs.Any(p => p.Id == commentId);
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments_dbs.Where(p => p.Id == commentId).FirstOrDefault();
        }

        public ICollection<Comment> GetComments()
        {
            return _context.Comments_dbs.ToList();
        }

        public ICollection<Comment> GetCommentsOfABlogPost(int blogPostId)
        {
            // the below code, would return a List of BlogPost, not Comments, as specified in the returning Type in this method
            //return _context.Comments_dbs.Where(p => p.BlogPost.Id == blogPostId).Select(bp => bp.BlogPost).ToList();

            // returning a List of comments, as specified in the retuning type of the method here
            return _context.Comments_dbs.Where(p => p.BlogPost.Id == blogPostId).ToList();
        }
    }
}
