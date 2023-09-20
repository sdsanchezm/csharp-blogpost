using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAuthorController : Controller
    {
        private readonly IPostAuthorService _postAuthorService;
        public PostAuthorController(IPostAuthorService postAuthorService)
        {
            _postAuthorService = postAuthorService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PostAuthor>))]
        //[ProducesResponseType(400)]
        public IActionResult GetAuthors()
        {
            var authors = _postAuthorService.GetPostAuthors();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        [ProducesResponseType(200, Type = typeof(PostAuthor))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthor(int authorId)
        {
            if (!_postAuthorService.PostAuthorExist(authorId))
                return NotFound();

            var au = _postAuthorService.GetPostAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(au);
        }

        [HttpGet("{authorId}/blogpost")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BlogPost>))]
        [ProducesResponseType(400)]
        public IActionResult GetBlogPostByAuthor(int authorId)
        {

            if (!_postAuthorService.PostAuthorExist(authorId))
                return NotFound();

            var p = _postAuthorService.GetBlogPostByPostAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(p);
        }

    }
}
