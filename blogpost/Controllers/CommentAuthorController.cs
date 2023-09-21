using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentAuthorController : Controller
    {

        ICommentAuthorService _commentAuthorService;
        public CommentAuthorController(ICommentAuthorService commentAuthorService)
        {
            _commentAuthorService = commentAuthorService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentAuthor>))]
        public IActionResult GetCommentAuthors()
        {
            var ca = _commentAuthorService.GetCommentAuthors();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ca);
        }

        [HttpGet("{commentAuthorId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentAuthor(int commentAuthorId) 
        {
            if (!_commentAuthorService.ExistCommentAuthor(commentAuthorId))
                return NotFound();

            var ca = _commentAuthorService.GetCommentAuthor(commentAuthorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ca);
        }

        [HttpGet("{commentAuthorId}/comments")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByCommentAuthor(int commentAuthorId)
        {
            if (!_commentAuthorService.ExistCommentAuthor(commentAuthorId))
                return NotFound();

            var c = _commentAuthorService.GetCommentsByCommentAuthor(commentAuthorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }
    }
}
