using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.EntityFrameworkCore;

namespace blogpost.Services
{
    public class CommentAuthorService : ICommentAuthorService
    {
        private readonly DataContext _context;

        public CommentAuthorService(DataContext context)
        {
            _context = context;
        }

        public bool ExistCommentAuthor(int commentAuthorId)
        {
            return _context.CommentAuthors_dbs.Any(p => p.Id == commentAuthorId);
        }

        public CommentAuthor GetCommentAuthor(int commentAuthorId)
        {
            return _context.CommentAuthors_dbs.Where(p => p.Id == commentAuthorId).Include(ca => ca.Comments).FirstOrDefault();
        }

        public ICollection<CommentAuthor> GetCommentAuthors()
        {
            return _context.CommentAuthors_dbs.ToList();
        }

        public ICollection<Comment> GetCommentsByCommentAuthor(int commentAuthorId)
        {
            return _context.Comments_dbs.Where(p => p.Commenter.Id == commentAuthorId).ToList();
        }
    }
}
