using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;

namespace blogpost.Services
{
    public class BlogPostService : IBlogPostService
    {
        DataContext _dataContext;
        public BlogPostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<BlogPost> GetBlogPosts()
        {
            return _dataContext.BlogPosts_dbs.OrderBy(p => p.Id).ToList();
        }
    }
}
