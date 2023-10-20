using System.ComponentModel.DataAnnotations;

namespace blogpost.Dto.CreateDto
{
    public class CreateCommentDtoIn
    {
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }
        public int postId { get; set; }
        public int commentAuthorId { get; set; }
    }
}