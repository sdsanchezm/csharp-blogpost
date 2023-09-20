using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<City>))]
        public IActionResult GetCities()
        {
            var c = _cityService.GetCities();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("{cityId}")]
        [ProducesResponseType(200, Type = typeof(City))]
        public IActionResult GetCity(int cityId)
        {
            if (!_cityService.CityExist(cityId))
                return NotFound();

            var c = _cityService.GetCity(cityId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        [HttpGet("author/{authorId}")]
        [ProducesResponseType(200, Type = typeof(City))]
        [ProducesResponseType(400)]
        public IActionResult GetCityByAuthor(int authorId)
        {
            //if (_cityService.)

            var c = _cityService.GetCityByAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(c);
        }

        //[HttpGet("/authors/{authorId}")]
        //[ProducesResponseType(200, Type = typeof(City))]
        //[ProducesResponseType(400)]
        //public IActionResult GetAuthorByCity(int id)
        //{

        //    if (_cityService.CityExist(id))
        //        return NotFound();

        //    var authors = _cityService.GetAuthorsByCity(id);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(authors);

        //}
    }
}
