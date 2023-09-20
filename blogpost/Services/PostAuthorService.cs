using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;

namespace blogpost.Services
{
    public class PostAuthorService : IPostAuthorService
    {
        private readonly DataContext _context;

        public PostAuthorService(DataContext postAuthorService)
        {
            _context = postAuthorService;
        }

        public ICollection<BlogPost> GetBlogPostByPostAuthor(int postAuthorId)
        {
            var bp =_context.BlogPostPostauthors_dbs.Where(p => p.PostAuthorJT.Id == postAuthorId).Select(bp => bp.BlogPostJT).ToList();
            return bp;
        }

        public PostAuthor GetPostAuthor(int postAuthorId)
        {
            var pa = _context.PostAuthors_dbs.Where(p => p.Id == postAuthorId).FirstOrDefault();
            return pa;
        }

        public ICollection<PostAuthor> GetPostAuthorByBlogPostId(int blogPostId)
        {
            var bp = _context.BlogPostPostauthors_dbs.Where(p => p.BlogPostJT.Id == blogPostId).Select(pa => pa.PostAuthorJT).ToList();
            return bp;
        }

        public ICollection<PostAuthor> GetPostAuthors()
        {
            var pa = _context.PostAuthors_dbs.ToList();
            return pa;
        }

        public bool PostAuthorExist(int postAuthorId)
        {
            var p = _context.PostAuthors_dbs.Any(p => p.Id == postAuthorId);
            return p;
        }
    }
}
