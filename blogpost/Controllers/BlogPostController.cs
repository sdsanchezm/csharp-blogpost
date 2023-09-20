using blogpost.Dto;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : Controller
    {

        private readonly IBlogPostService _blogPostService;
        //private readonly IActionResultTypeMapper _mapper;
        //public BlogPostController(IBlogPostService blogPostService, IMapper mapper)
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
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


    }
}
