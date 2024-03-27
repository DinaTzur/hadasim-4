using CoronaAPI.DAL;
using CoronaAPI.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public ReportsController(CoronaAPIContext context)
        {
            _context = context;
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public async Task<int> GetAsync()
        {
            return await PatientManager.GetPatientWithNoVaccinationsCountAsync(_context);
        }
    }
}
