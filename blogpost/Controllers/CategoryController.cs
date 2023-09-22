using blogpost.Dto;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        // for mapper, have to inject the mapper object here
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public ActionResult GetCategories()
        {
            var c = _categoryService.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public ActionResult GetCategory(int categoryId)
        {
            if (!_categoryService.CategoryExist(categoryId))
            return NotFound();

            // using mapper:
            // var category = _mapper.Map<>(_categoryService.GetCategory(categoryId));

            var c = _categoryService.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("blogpost/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public ActionResult GetBlogPostByCategory(int categoryId)
        {
            // for mapper, in case implemented:
            //var b = _mapper.Map<>(_categoryService.GetBlogPostByCategory(categoryId));

            var b = _categoryService.GetBlogPostByCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryNew)
        {
            if (categoryNew == null)
                return BadRequest(ModelState);

            var categoryLocal = _categoryService.GetCategories()
                .Where(p => p.CategoryName.ToUpper() == categoryNew.CategoryName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (categoryLocal != null)
            {
                ModelState.AddModelError("", "Category already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_categoryService.CreateCategory(categoryNew))
            {
                ModelState.AddModelError("", "Something went wrong while saving...");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created.");


        }

    }
}
