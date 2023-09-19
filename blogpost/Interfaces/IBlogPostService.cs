using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface IBlogPostService
    {
        ICollection<BlogPost> GetBlogPosts();
    }
}