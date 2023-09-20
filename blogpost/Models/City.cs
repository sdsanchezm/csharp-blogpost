namespace blogpost.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public ICollection<PostAuthor> Authors { get; set;}
    }
}