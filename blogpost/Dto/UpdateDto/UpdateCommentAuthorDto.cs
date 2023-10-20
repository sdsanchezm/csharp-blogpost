using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto.UpdateDto
{
    public class UpdateCommentAuthorDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
