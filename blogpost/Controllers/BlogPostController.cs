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
            return Ok(posts);
        }
    }
}
