using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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

    }
}
