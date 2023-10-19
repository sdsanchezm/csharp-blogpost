using blogpost.Dto;
using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostAuthorController : Controller
    {
        private readonly IPostAuthorService _postAuthorService;
        private readonly ICityService _cityService;
        public PostAuthorController(IPostAuthorService postAuthorService, ICityService cityService)
        {
            _postAuthorService = postAuthorService;
            _cityService = cityService;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePostAuthor([FromBody] PostAuthorDto postAuthorNew)
        {
            if (postAuthorNew == null)
                return BadRequest(ModelState);

            var postAuthorLocal = _postAuthorService.GetPostAuthors()
                .Where(p => p.AuthorUsername.Trim().ToUpper() == postAuthorNew.AuthorUsername.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (postAuthorLocal != null)
            {
                ModelState.AddModelError("", "City already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            var cityId = postAuthorNew.CityId;
            if (!_cityService.CityExist(cityId))
                return NotFound("City Not Found");

            var cityOfauthor = _cityService.GetCity(cityId);

            var npa = new PostAuthor
            {
                AuthorUsername = postAuthorNew.AuthorUsername,
                FavLanguage = postAuthorNew.FavLanguage,
                AuthorPostCity = cityOfauthor
            };

            if (!_postAuthorService.CreatePostAuthor(npa))
            {
                ModelState.AddModelError("", "Error while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created and saved.");

        }

    }
}
