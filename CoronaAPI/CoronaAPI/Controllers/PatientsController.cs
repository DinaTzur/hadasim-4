using Microsoft.AspNetCore.Mvc;
using CoronaAPI.Data;
using CoronaAPI.Models;
using CoronaAPI.BL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public PatientsController(CoronaAPIContext context)
        {
            _context = context;
        }
        
        // GET: api/Patients
        [HttpGet]
        public async Task<IEnumerable<Patient>> GetPatient()
        {
            PatientService service = new PatientService(_context);
            return await service.GetAllPatientsAsync();
            
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            PatientService service = new PatientService(_context);
            Patient? patient = await service.GetPatientAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            PatientService service = new PatientService(_context);
            Result<Patient> result = await service.CreateAsync(patient);

            if (result.Status == ResultsEnum.Success)
            {
                return CreatedAtAction("PostPatient", new { id = patient.Id }, patient);
            }
            
            return BadRequest(result);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            try
            {
                PatientService service = new PatientService(_context);
                patient = await service.UpdatePatientAsync(patient, id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();

            }
            catch (ArgumentException)
            {   
              return BadRequest();
            }

            return NoContent();
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                PatientService service = new PatientService(_context);
                await service.DeletePatientAsync(id);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
