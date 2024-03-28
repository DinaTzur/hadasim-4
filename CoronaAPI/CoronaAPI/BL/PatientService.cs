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

        //Returning comments from normality tests
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

        //Display of all patients belonging to the cash register
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await PatientManager.GetAllPatientsAsync(_context);
        }
        
        //Showing a specific patient according to the ID card is the key
        public async Task<Patient?> GetPatientAsync(int id)
        {
            return await PatientManager.GetPatientByIdAsync(_context, id);
        }

        //Updating personal details of a patient
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

        //Deletion of a patient from the HMO database, including vaccinations and infection
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
