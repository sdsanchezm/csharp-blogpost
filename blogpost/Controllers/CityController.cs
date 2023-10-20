using blogpost.Dto;
using blogpost.Interfaces;
using blogpost.Models;
using blogpost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

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

        [HttpGet("author/{cityId}")]
        [ProducesResponseType(200, Type = typeof(City))]
        [ProducesResponseType(400)]
        public IActionResult getAuthorByCity(int cityId)
        {
            if (_cityService.CityExist(cityId))
                return NotFound();

            var authors = _cityService.GetAuthorsByCity(cityId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCity([FromBody] CityDto cityNew)
        {
            if (cityNew == null)
                return BadRequest(ModelState);

            var cityLocal = _cityService.GetCities()
                .Where(p => p.CityName.Trim().ToUpper() == cityNew.CityName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (cityLocal != null)
            {
                ModelState.AddModelError("", "City already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest();

            // instead of 
            var nc = new City
            {
                CityName = cityNew.CityName,
            };

            if (!_cityService.CreateCity(nc))
            {
                ModelState.AddModelError("", "Error while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created and saved.");
        }

        [HttpPut("{cityId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int cityId, [FromBody] CityDto cityUpdate)
        {
            if (cityUpdate == null)
                return BadRequest(ModelState);

            if (cityId != cityUpdate.Id)
            {
                ModelState.AddModelError("error", "there was an error with your request.");
                return BadRequest(ModelState);
            }

            if (!_cityService.CityExist(cityId))
                return NotFound("City was not found.");

            if (!ModelState.IsValid)
                return BadRequest();

            var c = _cityService.GetCity(cityId);

            c.CityName = cityUpdate.CityName;

            if (!_cityService.UpdateCity(c))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            // return NoContent();
            return Ok("Resource was updated successfully.");
        }


    }
}
