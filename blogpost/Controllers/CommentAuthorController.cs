using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using blogpost.Dto.CreateDto;
using blogpost.Dto.UpdateDto;
using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Services;

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePostAuthor([FromBody] CreateCommentAuthorDtoIn commentAuthorNew)
        {
            if (commentAuthorNew == null)
                return BadRequest(ModelState);

            if (_commentAuthorService.ExistCommentAuthorByUsername(commentAuthorNew.Username))
            {
                ModelState.AddModelError("", "Comment Author already exist.");
                return StatusCode(422, ModelState);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest();

            // create a new Comment author
            var nca = new CommentAuthor
            {
                Username = commentAuthorNew.Username
            };

            if (!_commentAuthorService.CreateCommentAuthor(nca))
            {
                ModelState.AddModelError("", "ERROR, Data not saved.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created and saved.");
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCommentAuthor(int authorId, [FromBody] UpdateCommentAuthorDto updateCommentAuthor)
        {
            if (updateCommentAuthor == null)
            {
                ModelState.AddModelError("", "Data missing.");
                return BadRequest(ModelState);
            }

            if (authorId != updateCommentAuthor.Id)
            {
                ModelState.AddModelError("", "Id differs.");
                return BadRequest(ModelState);
            }

            if (!_commentAuthorService.ExistCommentAuthor(authorId))
                return NotFound("Entity does not exist.");

            if (!ModelState.IsValid)
                return BadRequest("Error ocurred.");

            var ca = _commentAuthorService.GetCommentAuthor(authorId);
            ca.Username = updateCommentAuthor.Username;

            if (!_commentAuthorService.UpdateCommentAuthor(ca))
            {
                ModelState.AddModelError("", "Something went wrong updating blog post");
                return StatusCode(500, ModelState);
            }

            // return NoContent();
            return Ok("Record Saved.");
        }

    }
}
