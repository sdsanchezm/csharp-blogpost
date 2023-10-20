using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto.CreateDto
{
    public class CreateCommentAuthorDtoIn
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
