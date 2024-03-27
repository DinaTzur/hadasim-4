using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoronaAPI.DAL
{
    public class PatientVaccinationManager
    {
        public PatientVaccinationManager() { }

        public static async Task<PatientVaccination> AddPatientVaccination(CoronaAPIContext context, PatientVaccination patientVaccination)
        {
            context.PatientVaccinations.Add(patientVaccination);
            await context.SaveChangesAsync();
            return patientVaccination;
        }

        public static async Task<IEnumerable<PatientVaccination>> GetPatientVaccinationsForPatientAsync(CoronaAPIContext context, int patientId)
        {
            return await context.PatientVaccinations
                                .Where(p => p.PatientId == patientId)
                                .Include(x => x.Manufactor)
                                .ToListAsync();
        }

        public static async Task<PatientVaccination?> GetPatientVaccinationByIdAsync(CoronaAPIContext context, int vaccinationId)
        {
            return await context.PatientVaccinations.FirstOrDefaultAsync(p => p.Id == vaccinationId);
        }

        public static async Task<int> PatientVaccinationsCountForPationtAsync(CoronaAPIContext context, int patientId)
        {
            return await context.PatientVaccinations
                          .Where(e => e.PatientId == patientId)
                          .CountAsync();
        }

        public static async Task<bool> PatientVaccinationsExistsAsync(CoronaAPIContext context, int? id = null)
        {
            return await context.PatientVaccinations.AnyAsync(e => e.Id == id);
        }

        public static async Task<PatientVaccination> UpdatePatientVaccinationAsync(CoronaAPIContext context, PatientVaccination patientVaccination)
        {
            context.Entry(patientVaccination).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return patientVaccination;
        }

        public static async Task DeletePatientVaccination(CoronaAPIContext context, PatientVaccination patientVaccination)
        {
            context.PatientVaccinations.Remove(patientVaccination);
            await context.SaveChangesAsync();
        }
    }
}
