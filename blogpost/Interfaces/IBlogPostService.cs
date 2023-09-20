using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface IBlogPostService
    {
        ICollection<BlogPost> GetBlogPosts();
        BlogPost GetBlogPost(int blogPostId);
        BlogPost GetBlogPost(string blogPostTitle);
        decimal GetBlogPostAverageRate(int blogPostId);
        bool BlogPostExists(int blogPostId);

    }
}