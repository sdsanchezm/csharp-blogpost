using Microsoft.AspNetCore.Mvc;
using blogpost.Dto.CreateDto;
using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Services;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogPostService _blogPostService;
        private readonly ICommentAuthorService _commentAuthorService;
        public CommentController(ICommentService commentService, IBlogPostService blogPostService, ICommentAuthorService commentAuthorService)
        {
            _commentService = commentService;
            _blogPostService = blogPostService;
            _commentAuthorService = commentAuthorService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetComments()
        {
            var c = _commentService.GetComments();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int commentId)
        {
            if (!_commentService.ExistComment(commentId))
                return NotFound();

            var c = _commentService.GetComment(commentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("blogpost/{blogPostId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsOfABlogPost(int blogPostId)
        {

            //if (!_commentService.BlogPostExists(blogPostId))
            //    return NotFound();

            var comments = _commentService.GetCommentsOfABlogPost(blogPostId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment([FromBody] CreateCommentDtoIn commentNew)
        {
            if (commentNew == null)
                return BadRequest(ModelState);

            //if (!ModelState.IsValid)
            //    return BadRequest();

            if (_blogPostService.GetBlogPost(commentNew.postId) == null)
            {
                return NotFound("Post Not Found.");
            }

            if (_commentAuthorService.GetCommentAuthor(commentNew.commentAuthorId) == null)
            {
                return NotFound("Comment Author Not Found.");
            }

            // create a new Comment
            var nc = new Comment
            {
                CommentTitle = commentNew.CommentTitle,
                CommentContent = commentNew.CommentContent,
                Rate = commentNew.Rate,
            };

            if (!_commentService.CreateComment(commentNew.commentAuthorId, commentNew.postId, nc))
            {
                ModelState.AddModelError("", "ERROR, Data not saved.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created and saved.");

        }

    }
}
