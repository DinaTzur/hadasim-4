using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoronaAPI.Data;
using CoronaAPI.Models;
using CoronaAPI.BL;

namespace CoronaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientVaccinationsController : ControllerBase
    {
        private readonly CoronaAPIContext _context;

        public PatientVaccinationsController(CoronaAPIContext context)
        {
            _context = context;
        }

        // GET: api/PatientVaccinations/5
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<PatientVaccination>>> GetPatientVaccination(int patientId)
        {
            PatientVaccinationService service = new PatientVaccinationService(_context);
            try
            {
                var patientVaccinations = await service.GetPatientVaccinationsForPatientAsync(patientId);
                return CreatedAtAction("GetPatientVaccination", value: patientVaccinations);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        // PUT: api/PatientVaccinations/5
        [HttpPut("{vaccinationId}")]
        public async Task<IActionResult> PutPatientVaccination(int vaccinationId, PatientVaccination patientVaccination)
        {
            try
            {
                PatientVaccinationService service = new PatientVaccinationService(_context);
                patientVaccination = await service.UpdatePatientVaccinationAsync(patientVaccination, vaccinationId);
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

        // POST: api/PatientVaccinations
        [HttpPost]
        public async Task<ActionResult<PatientVaccination>> PostPatientVaccination(PatientVaccination patientVaccination)
        {
            PatientVaccinationService service = new PatientVaccinationService(_context);
            Result<PatientVaccination> result = await service.CreatePatientVaccinationAsync(patientVaccination);

            if (result.Status == ResultsEnum.Success)
            {
                return CreatedAtAction("PostPatientVaccination", new { id = patientVaccination.Id }, patientVaccination);
            }

            return BadRequest(result);
        }

        // DELETE: api/PatientVaccinations/5
        [HttpDelete("{vaccinationId}")]
        public async Task<IActionResult> DeletePatientVaccination(int vaccinationId)
        {
            try
            {
                PatientVaccinationService service = new PatientVaccinationService(_context);
                await service.DeletePatientVaccinationAsync(vaccinationId);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();

            }

            return NoContent();
        }

    }
}
