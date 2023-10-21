using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto.UpdateDto
{
    public class UpdateCommentDto
    {
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }
    }
}