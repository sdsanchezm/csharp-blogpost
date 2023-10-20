using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto.CreateDto
{
    public class CreateBlogPostDtoIn
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BlogPostAuthorId { get; set; }
        
    }
}
