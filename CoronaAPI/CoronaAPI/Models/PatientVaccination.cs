using System.Text.Json.Serialization;

namespace CoronaAPI.Models
{
    public class PatientVaccination
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ManufactorId { get; set; }
        public DateTime VaccinationDate { get; set; }
        
        public Manufactor? Manufactor { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }

    }
}
