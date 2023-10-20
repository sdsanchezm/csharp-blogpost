namespace blogpost.Dto.UpdateDto
{
    public class UpdateBlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int authorId { get; set; }
        public int categoryId { get; set; }
    }
}
