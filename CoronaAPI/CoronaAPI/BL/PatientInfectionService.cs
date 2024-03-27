using CoronaAPI.DAL;
using CoronaAPI.Data;
using CoronaAPI.Models;

namespace CoronaAPI.BL
{
    public class PatientInfectionService
    {
        private CoronaAPIContext _context;
        public PatientInfectionService(CoronaAPIContext context)
        {
            _context = context;
        }  

        public async Task<Result<PatientInfection>> CreateOrUpdatePatientInfectionAsync(PatientInfection patientInfection)
        {
            if (!await PatientManager.PatientExistsAsync(_context, patientInfection.PatientId))
            {
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.NotFound,
                    Message = "Patient not exists",
                };
            }

            if (patientInfection.DateRecovery.HasValue && !Validator.ValidateDateRange(patientInfection.DatePositive, patientInfection.DateRecovery.Value))
            {
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.Failure,
                    Message = "Date Recovery must be greater then Date Positive",
                };
            }

            if (!Validator.ValidateDateRange(patientInfection.DatePositive, DateTime.Now))
            {
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.Failure,
                    Message = "Date Positive can't be greater then today",
                };
            }

            if (patientInfection.DateRecovery.HasValue && !Validator.ValidateDateRange(patientInfection.DateRecovery.Value, DateTime.Now))
            {
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.Failure,
                    Message = "Date Recovery can't be greater then today",
                };
            }

            try
            {
                PatientInfection res = await PatientInfectionManager.AddOrUpdatePatientInfection(_context, patientInfection);
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.Success,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new Result<PatientInfection>
                {
                    Status = ResultsEnum.Failure,
                    Message = ex.Message
                };
            }
        }

        public async Task<PatientInfection?> GetPatientInfectionAsync(int patientId)
        {
            return await PatientInfectionManager.GetPatientInfectionByPatientIdAsync(_context, patientId);
        }

        public async Task DeletePatientInfectionAsync(int patientId)
        {
            PatientInfection? patientInfection = await PatientInfectionManager.GetPatientInfectionByPatientIdAsync(_context, patientId);
            if (patientInfection == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            await PatientInfectionManager.DeletePatientInfection(_context, patientInfection);
        }
    }
}
