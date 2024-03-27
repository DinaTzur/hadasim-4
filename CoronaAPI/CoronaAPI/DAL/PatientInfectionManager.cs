using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CoronaAPI.DAL
{
    public class PatientInfectionManager
    {
        public PatientInfectionManager() { }

        public static async Task<PatientInfection> AddOrUpdatePatientInfection(CoronaAPIContext context, PatientInfection patientInfection)
        {
            if(await PatientInfectionExistsAsync(context, patientInfection.PatientId))
            {
                context.Entry(patientInfection).State = EntityState.Modified;
            }
            else
            {
                context.PatientInfections.Add(patientInfection);
            }

            await context.SaveChangesAsync();
            return patientInfection;
        }

        public static async Task<PatientInfection?> GetPatientInfectionByPatientIdAsync(CoronaAPIContext context, int patientId)
        {
            return await context.PatientInfections
                                .FirstOrDefaultAsync(p => p.PatientId == patientId);

        }

        public static async Task<bool> PatientInfectionExistsAsync(CoronaAPIContext context, int patientId)
        {
            return await context.PatientInfections.AnyAsync(e => e.PatientId == patientId);
        }

        public static async Task DeletePatientInfection(CoronaAPIContext context, PatientInfection patientInfection)
        {
            context.PatientInfections.Remove(patientInfection);
            await context.SaveChangesAsync();
        }
    }
}
