using CoronaAPI.DAL;
using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public CitiesController(CoronaAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<City>> Get()
        {
            return await CityManager.GetAllCitiesAsync(_context);
        }
    }
}
