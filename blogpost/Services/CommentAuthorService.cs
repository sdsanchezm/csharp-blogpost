using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;

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
            //return _context.CommentAuthors_dbs.Where(p => p.Id == commentAuthorId).Include(ca => ca.Comments).FirstOrDefault();
            return _context.CommentAuthors_dbs.Where(p => p.Id == commentAuthorId).FirstOrDefault();
        }

        public ICollection<CommentAuthor> GetCommentAuthors()
        {
            return _context.CommentAuthors_dbs.ToList();
        }

        public ICollection<Comment> GetCommentsByCommentAuthor(int commentAuthorId)
        {
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve, // to handle cyclic references
            //};

            var c = _context.Comments_dbs.Where(p => p.Commenter.Id == commentAuthorId).ToList();

            // serialzie comments
            //var serializedComments = JsonSerializer.Serialize(c, options);

            //return serializedComments;
            return c;

        }

        public bool CreateCommentAuthor(CommentAuthor commentAuthor)
        {
            _context.Add(commentAuthor);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ExistCommentAuthorByUsername(string username)
        {
            var res = _context.CommentAuthors_dbs.Where(p => p.Username == username).FirstOrDefault();

            if (res != null)
                return true;

            return false;
        }

        public bool UpdateCommentAuthor(CommentAuthor commentAuthor)
        {
            _context.Update(commentAuthor);
            return Save();
        }

        public bool DeleteCommentAuthor(int commentAuthorId)
        {
            var ca = _context.CommentAuthors_dbs.Where(p => p.Id == commentAuthorId).FirstOrDefault();
            _context.Remove(ca);
            return Save();
        }
    }
}
