using Microsoft.AspNetCore.Mvc;
using CoronaAPI.Data;
using CoronaAPI.Models;
using CoronaAPI.BL;

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInfectionsController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public PatientInfectionsController(CoronaAPIContext context)
        {
            _context = context;
        }

        // GET: api/PatientInfections/5
        [HttpGet("{patientId}")]
        public async Task<ActionResult<PatientInfection>> GetPatientInfection(int patientId)
        {
            PatientInfectionService service = new PatientInfectionService(_context);
            PatientInfection? PatientInfection = await service.GetPatientInfectionAsync(patientId);

            if (PatientInfection == null)
            {
                return NotFound();
            }

            return PatientInfection;
        }

        // POST: api/PatientInfections
        [HttpPost]
        public async Task<ActionResult<PatientInfection>> PostPatientInfection(PatientInfection patientInfection)
        {
            PatientInfectionService service = new PatientInfectionService(_context);
            Result<PatientInfection> result = await service.CreateOrUpdatePatientInfectionAsync(patientInfection);

            if (result.Status == ResultsEnum.Success)
            {
                return CreatedAtAction("PostPatientInfection", new { patientId = patientInfection.PatientId }, patientInfection);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
