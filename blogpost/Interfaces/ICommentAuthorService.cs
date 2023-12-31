﻿using blogpost.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace blogpost.Interfaces
{
    public interface ICommentAuthorService
    {
        ICollection<CommentAuthor> GetCommentAuthors();
        bool ExistCommentAuthor(int commentAuthorId);
        bool ExistCommentAuthorByUsername(string username);
        CommentAuthor GetCommentAuthor(int commentAuthorId);
        ICollection<Comment> GetCommentsByCommentAuthor(int commentAuthorId);
        public bool CreateCommentAuthor(CommentAuthor commentAuthor);
        bool UpdateCommentAuthor(CommentAuthor commentAuthor);
        bool DeleteCommentAuthor(int commentAuthorId);
        public bool Save();
    }
}
