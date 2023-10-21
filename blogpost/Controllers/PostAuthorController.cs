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

        [HttpPut("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAuthor(int authorId, [FromBody] PostAuthorDto postAuthorUpdate)
        {
            if (postAuthorUpdate == null)
                return BadRequest(ModelState);

            if (authorId != postAuthorUpdate.Id)
                return BadRequest(ModelState);

            if (!_postAuthorService.PostAuthorExist(authorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var pa = _postAuthorService.GetPostAuthor(postAuthorUpdate.Id);
            var city = _cityService.GetCity(postAuthorUpdate.CityId);

            pa.AuthorUsername = postAuthorUpdate.AuthorUsername;
            pa.FavLanguage = postAuthorUpdate.FavLanguage;
            pa.AuthorPostCity = city;

            if (!_postAuthorService.UpdatePostAuthor(pa))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            // return NoContent();
            return Ok("Resource was saved.");
        }

        [HttpDelete("{postAuthorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int postAuthorId)
        {
            if (!_postAuthorService.PostAuthorExist(postAuthorId))
            {
                return NotFound("Resource not found.");
            }

            if (_postAuthorService.GetBlogPostByPostAuthor(postAuthorId).ToArray().Length != 0)
            {
                return BadRequest("Resource in use, cannot be deleted.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postAuthorService.DeletePostAuthor(postAuthorId))
            {
                ModelState.AddModelError("Error", "Something went wrong deleting Post Author.");
            }

            // return NoContent();
            return Ok("record deleted.");
        }


    }
}
