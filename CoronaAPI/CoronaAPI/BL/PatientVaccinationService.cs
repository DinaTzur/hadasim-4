using CoronaAPI.DAL;
using CoronaAPI.Data;
using CoronaAPI.Models;

namespace CoronaAPI.BL
{
    public class PatientVaccinationService
    {
        private CoronaAPIContext _context;
        public PatientVaccinationService(CoronaAPIContext context)
        {
            _context = context;
        }

        public async Task <Result<PatientVaccination>> CreatePatientVaccinationAsync(PatientVaccination patientVaccination) 
        {
            if (await PatientVaccinationManager.PatientVaccinationsCountForPationtAsync(_context, patientVaccination.PatientId)>=4)
            {
                throw new ArgumentException();
            }

            if (!Validator.ValidateDateRange(patientVaccination.VaccinationDate, DateTime.Now))
            {
                return new Result<PatientVaccination>
                {
                    Status = ResultsEnum.Failure,
                    Message = "Vaccination date can't be greater then today",
                };
            }

            try
            {
                PatientVaccination res = await PatientVaccinationManager.AddPatientVaccination(_context, patientVaccination);
                return new Result<PatientVaccination>
                {
                    Status = ResultsEnum.Success,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new Result<PatientVaccination>
                {
                    Status = ResultsEnum.Failure,
                    Message = ex.Message
                };
            }
        }

        public async Task<IEnumerable<PatientVaccination>> GetPatientVaccinationsForPatientAsync(int patientId)
        {
            if (!await PatientManager.PatientExistsAsync(_context, patientId))
            {
                throw new ArgumentOutOfRangeException();
            }

            return await PatientVaccinationManager.GetPatientVaccinationsForPatientAsync(_context, patientId);
        }

        public async Task<PatientVaccination> UpdatePatientVaccinationAsync(PatientVaccination patientVaccination, int vaccinationId)
        {
            if (vaccinationId != patientVaccination.Id)
            {
                throw new ArgumentException();
            }

            if (!await PatientVaccinationManager.PatientVaccinationsExistsAsync(_context, vaccinationId))
            {
                throw new ArgumentOutOfRangeException();
            }

            return await PatientVaccinationManager.UpdatePatientVaccinationAsync(_context, patientVaccination);
        }

        public async Task DeletePatientVaccinationAsync(int vaccinationId)
        {
            PatientVaccination? patientVaccination = await PatientVaccinationManager.GetPatientVaccinationByIdAsync(_context, vaccinationId);
            if (patientVaccination == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            await PatientVaccinationManager.DeletePatientVaccination(_context, patientVaccination);
        }
    }
}
