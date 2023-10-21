using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface IPostAuthorService
    {
        ICollection<PostAuthor> GetPostAuthors();
        PostAuthor GetPostAuthor(int postAuthorId);
        bool PostAuthorExist(int postAuthorId);
        ICollection<PostAuthor> GetPostAuthorByBlogPostId(int blogPostId);
        ICollection<BlogPost> GetBlogPostByPostAuthor(int postAuthorId);
        bool CreatePostAuthor(PostAuthor postAuthorNew);
        bool UpdatePostAuthor(PostAuthor postAuthorUpdate);
        bool DeletePostAuthor(int postAuthorId);
        bool Save();
    }
}
