using blogpost.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : Controller
    {

        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public IActionResult GetBlogPosts()
        {
            var posts = _blogPostService.GetBlogPosts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet]
        public IActionResult GetBlogPost(int id)
        {
            var post = _blogPostService.GetBlogPost();
            return Ok(post);
        }


    }
}
