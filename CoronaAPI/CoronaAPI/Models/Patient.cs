using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoronaAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Manufacturer ID cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Za-z \-'.]{2,20}$")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Manufacturer ID cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Za-z \-'.]{2,20}$")]
        public string LastName { get; set; }   
        
        public int CityId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Manufacturer ID cannot exceed 20 characters.")]
        [RegularExpression(@"^[A-Za-z \-'.]{2,20}$")]
        public string Address { get; set; }

        [Required]
        [Range(0, 1000)]
        public int HouseNumber { get; set; }
        public DateTime DateBirth { get; set; }

        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        [RegularExpression(@"^0[23489]-?\d{7}$")]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Mobile Phone number cannot exceed 11 characters.")]
        [RegularExpression(@"^05\d-?\d{7}$")]
        public string? MobilePhone { get; set; }

        public City? City { get; set; }
        [JsonIgnore]
        public PatientInfection? Infection { get; set; }
        [JsonIgnore]
        public ICollection<PatientVaccination>? Vaccinations { get;}
    }
}
