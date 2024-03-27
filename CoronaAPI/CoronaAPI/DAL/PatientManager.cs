using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoronaAPI.DAL
{
    public class PatientManager
    {
        public PatientManager() { }

        public static async Task<Patient> AddPatient(CoronaAPIContext context, Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            return patient;
        }

        public static async Task<IEnumerable<Patient>> GetAllPatientsAsync(CoronaAPIContext context)
        {
            return await context.Patients.ToListAsync();
        }

        public static async Task<Patient?> GetPatientByIdAsync(CoronaAPIContext context, int id)
        {
            return await context.Patients.Include(x => x.City).FirstOrDefaultAsync(p => p.Id == id);
        }

        public static async Task<bool> PatientExistsAsync(CoronaAPIContext context, int id)
        {
            return await context.Patients.AnyAsync(e => e.Id == id);
        }

        public static async Task<Patient> UpdatePatientAsync(CoronaAPIContext context, Patient patient)
        {
            context.Entry(patient).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return patient;
        }

        public static async Task<int> GetPatientWithNoVaccinationsCountAsync(CoronaAPIContext context)
        {
            return await context.Patients.CountAsync(x=>x.Vaccinations.Count==0);
        }

        public static async Task DeletePatient(CoronaAPIContext context, Patient patient)
        {
            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
        }
    }
}
