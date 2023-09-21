using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

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
        //[ProducesResponseType(200, Type = typeof(Comment))]
        //[ProducesResponseType(400)]
        public IActionResult GetCommentsByCommentAuthor(int commentAuthorId)
        {
            if (!_commentAuthorService.ExistCommentAuthor(commentAuthorId))
                return NotFound();

            // one way to resolve this issue
            //var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };

            var c = _commentAuthorService.GetCommentsByCommentAuthor(commentAuthorId);

            //var serializedComments = JsonSerializer.Serialize(c, options);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //return Ok(serializedComments);
            return Ok(c);
        }
    }
}
