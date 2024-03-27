using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoronaAPI.Models
{
    public class PatientInfection
    {
        [Key]
        public int PatientId { get; set; }
        public DateTime DatePositive { get; set; }
        public DateTime? DateRecovery { get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; }

    }
}
