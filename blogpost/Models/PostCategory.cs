namespace blogpost.Models
{
    public class PostCategory
    {
        public int BlogPostId { get; set; }
        public int CategoryId { get; set; }
        public BlogPost BlogPostJT { get; set; }
        public Category CategoryJT { get; set; }
    }
    
}