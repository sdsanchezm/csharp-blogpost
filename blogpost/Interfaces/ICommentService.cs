using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface ICommentService
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int commentId);
        bool ExistComment(int commentId);
        ICollection<Comment> GetCommentsOfABlogPost(int blogPostId);
        bool CreateComment(int commenterId, int postId, Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(int commentId);
        bool Save();
    }
}
