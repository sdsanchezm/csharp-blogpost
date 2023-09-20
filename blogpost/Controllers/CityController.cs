using blogpost.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace blogpost.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
