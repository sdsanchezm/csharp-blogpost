namespace blogpost.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<PostCategory> PostCategories { get; set;}
    }
    
}