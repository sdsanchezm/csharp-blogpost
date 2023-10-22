using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using blogpost.Dto;
using blogpost.Dto.CreateDto;
using blogpost.Dto.UpdateDto;
using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Services;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : Controller
    {

        private readonly IBlogPostService _blogPostService;
        private readonly IPostAuthorService _postAuthorService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        //private readonly IActionResultTypeMapper _mapper;
        //public BlogPostController(IBlogPostService blogPostService, IMapper mapper)
        public BlogPostController(IBlogPostService blogPostService, ICategoryService categoryService, IPostAuthorService postAuthorService, ICommentService commentService)
        {
            _blogPostService = blogPostService;
            _categoryService = categoryService;
            _postAuthorService = postAuthorService;
            _commentService = commentService;
            //_mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBlogPosts()
        {
            // AutoMapper
            //var blogPost = _mapped.Map<List<BlogPostDto>>(_blogPostService.GetBlogPosts());

            // regular Service call:
            //var posts = _blogPostService.GetBlogPosts();

            //Dto used in here:
            var posts2 = from b in _blogPostService.GetBlogPosts()
                         select new BlogPostDto()
                         {
                             Id = b.Id,
                             Title = b.Title,
                             Content = b.Content,
                             CreationDate = b.CreationDate
                         };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts2);
        }

        [HttpGet("{blogPostId}")]
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(400)]
        public IActionResult GetBlogPost(int blogPostId)
        {
            if(!_blogPostService.BlogPostExists(blogPostId))
                return NotFound("no posts here...");

            var post = _blogPostService.GetBlogPost(blogPostId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(post);
        }

        [HttpGet("{blogPostId}/postrate")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetBlogPostRating(int blogPostId)
        {
            if (!_blogPostService.BlogPostExists(blogPostId))
                return NotFound("no posts here...");

            var rate_post = _blogPostService.GetBlogPostAverageRate(blogPostId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rate_post);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePostAuthor([FromBody] CreateBlogPostDtoIn blogPostNew)
        {
            if (blogPostNew == null)
                return BadRequest(ModelState);

            var blogPostLocal = _blogPostService.GetBlogPost(blogPostNew.Title);

            if (blogPostLocal != null)
            {
                ModelState.AddModelError("", "Post was created before, it exist already.");
                return StatusCode(422, ModelState);
            }

            //if (!ModelState.IsValid)
            //    return BadRequest();

            // create a new BlogPost
            var nbp = new BlogPost
            {
                Title = blogPostNew.Title,
                Content = blogPostNew.Content,
                CreationDate = DateTime.Now,
            };

            if (!_categoryService.CategoryExist(blogPostNew.CategoryId))
                return NotFound("Category Not Found.");

            if (!_postAuthorService.PostAuthorExist(blogPostNew.BlogPostAuthorId))
                return NotFound("Author Not Found.");

            if (!_blogPostService.CreateBlogPost(blogPostNew.BlogPostAuthorId, blogPostNew.CategoryId, nbp))
            {
                ModelState.AddModelError("", "ERROR, Data not saved.");
                return StatusCode(500, ModelState);
            }
                
            return Ok("Successfully created and saved.");
        }

        [HttpPut("{blogPostId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBlogPostPost(int blogPostId, [FromBody] UpdateBlogPostDto updatedBlogPost)
        {
            if (updatedBlogPost == null)
            {
                ModelState.AddModelError("", "Data missing.");
                return BadRequest(ModelState);
            }

            if (blogPostId != updatedBlogPost.Id)
            {
                ModelState.AddModelError("", "Id differs.");
                return BadRequest(ModelState);
            }

            if (!_blogPostService.BlogPostExists(blogPostId))
                return NotFound("Entity does not exist.");

            //if (!ModelState.IsValid)
            //    return BadRequest("Error ocurred.");

            var bp = _blogPostService.GetBlogPost(blogPostId);
            bp.Title = updatedBlogPost.Title;
            bp.Content = updatedBlogPost.Content;

            var authorId = updatedBlogPost.authorId;
            var categoryId = updatedBlogPost.categoryId;

            if (!_blogPostService.UpdateBlogPost(authorId, categoryId, bp))
            {
                ModelState.AddModelError("", "Something went wrong updating blog post");
                return StatusCode(500, ModelState);
            }

            // return NoContent();
            return Ok("Record Saved.");
        }

        [HttpDelete("{blogPostId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBlogPost(int blogPostId)
        {
            if (!_blogPostService.BlogPostExists(blogPostId))
            {
                return NotFound("The Post does not exist.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deleteComments = _commentService.GetCommentsOfABlogPost(blogPostId);

            if (!_commentService.DeleteComments(deleteComments.ToList()))
            {
                ModelState.AddModelError("Error", "Something went wrong when deleting comments");
            }

            if (!_blogPostService.DeleteBlogPost(blogPostId))
            {
                ModelState.AddModelError("Error", "Something went wrong deleting Blog Post");
            }

            // return NoContent();
            return Ok("Resource Deleted");
        }


    }
}
