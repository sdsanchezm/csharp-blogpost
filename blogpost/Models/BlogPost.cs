namespace blogpost.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Comment> PostComments { get; set; }
        public ICollection<BlogPostPostauthor> BlogPostPostauthors { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
    }

}
