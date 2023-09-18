namespace blogpost.Models
{
    public class PostCategory
    {
        public int BlogPostId { get; set; }
        public int CategoryId { get; set; }
        public BlogPost BlogPost { get; set; }
        public Category Category { get; set; }
    }
    
}