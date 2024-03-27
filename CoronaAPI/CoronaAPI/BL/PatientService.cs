using CoronaAPI.DAL;
using CoronaAPI.Data;
using CoronaAPI.Models;

namespace CoronaAPI.BL
{
    public class PatientService
    {
        private CoronaAPIContext _context;
        public PatientService(CoronaAPIContext context)
        {
            _context = context;
        }

        public async Task <Result<Patient>> CreateAsync(Patient patient) 
        {
            if (await PatientManager.PatientExistsAsync(_context, patient.Id))
            {
                return new Result<Patient>{
                    Status = ResultsEnum.Failure,
                    Message = "Patient already exists",
                };
            }

            if(!Validator.IsValidIsraeliId(patient.Id))
            {
                return new Result<Patient>
                {
                    Status = ResultsEnum.Failure,
                    Message = "Id not in correct format",
                };
            }

            try
            {
                Patient res = await PatientManager.AddPatient(_context, patient);
                return new Result<Patient>
                {
                    Status = ResultsEnum.Success,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new Result<Patient>
                {
                    Status = ResultsEnum.Failure,
                    Message = ex.Message
                };
            }
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await PatientManager.GetAllPatientsAsync(_context);
        }

        public async Task<Patient?> GetPatientAsync(int id)
        {
            return await PatientManager.GetPatientByIdAsync(_context, id);
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient, int id)
        {
            if (id != patient.Id)
            {
                throw new ArgumentException();
            }
            if (!await PatientManager.PatientExistsAsync(_context, patient.Id))
            {
                throw new ArgumentOutOfRangeException();
            }

            return await PatientManager.UpdatePatientAsync(_context, patient);
        }
        public async Task DeletePatientAsync(int id)
        {
            Patient? patient= await PatientManager.GetPatientByIdAsync(_context, id);
            if (patient == null)
            {
                throw new ArgumentOutOfRangeException();
            }

            await PatientManager.DeletePatient(_context, patient);
        }
    }
}
