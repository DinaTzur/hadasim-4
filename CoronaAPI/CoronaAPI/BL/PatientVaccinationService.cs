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

        //Insertion of new vaccine details for a specific patient
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

        //Displaying details of all vaccines associated with a specific patient
        public async Task<IEnumerable<PatientVaccination>> GetPatientVaccinationsForPatientAsync(int patientId)
        {
            if (!await PatientManager.PatientExistsAsync(_context, patientId))
            {
                throw new ArgumentOutOfRangeException();
            }

            return await PatientVaccinationManager.GetPatientVaccinationsForPatientAsync(_context, patientId);
        }

        //Updating specific vaccination details of a specific patient
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

        //Deleting specific vaccine data
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
