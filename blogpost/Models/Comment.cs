namespace blogpost.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }
        public int Rate { get; set; }
        public BlogPost BlogPost { get; set; }
        public CommentAuthor Commenter { get; set; }
    }
}