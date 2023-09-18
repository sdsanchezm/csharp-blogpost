namespace blogpost.Models
{
    public class PostAuthor
    {
        public int Id { get; set; }
        public string AuthorUsername { get; set; }
        public string FavLanguage { get; set; }
        public City AuthorPostCity { get; set; }
        public ICollection<BlogPostPostauthor> BlogPostPostauthors { get; set; }

    }
}