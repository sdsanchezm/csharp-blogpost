namespace blogpost.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Comment> PostComments { get; set; }
        public ICollection<BlogPostPostauthor> BlogPostPostauthor { get; set; }
        public ICollection<Category> PostCategories { get; set; }
    }

}
