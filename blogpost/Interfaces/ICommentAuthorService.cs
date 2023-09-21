using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface ICommentAuthorService
    {
        ICollection<CommentAuthor> GetCommentAuthors();
        bool ExistCommentAuthor(int commentAuthorId);
        CommentAuthor GetCommentAuthor(int commentAuthorId);
        ICollection<Comment> GetCommentsByCommentAuthor(int commentAuthorId);
    }
}
