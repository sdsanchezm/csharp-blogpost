namespace blogpost.Models
{
    public class BlogPostPostauthor
    {
        public int BlogPostId { get; set; }
        public int PostAuthorId { get; set; }
        public BlogPost BlogPostJT { get; set; }
        public PostAuthor PostAuthorJT { get; set; }
    }
}