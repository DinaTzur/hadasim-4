using CoronaAPI.DAL;
using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactorsController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public ManufactorsController(CoronaAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Manufactor>> Get()
        {
            return await ManufactorManager.GetAllManufactorsAsync(_context);
        }

  
    }
}
