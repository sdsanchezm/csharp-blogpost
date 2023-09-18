namespace blogpost.Models
{
    public class BlogPostPostauthor
    {
        public int BlogPostId { get; set; }
        public int PostAuthorId { get; set; }
        public BlogPost BlogPost { get; set; }
        public PostAuthor PostAuthor { get; set; }
    }
}