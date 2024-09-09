using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCS.Models
{
    public class Lecturer
    {
        public int LecturerID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)] // Adjust length as needed
        public string Password { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        public ICollection<Claim> Claims { get; set; }
        public string Role { get; set; } = "Lecturer"; 
        // Initialize the collection
        public Lecturer()
        {
            Claims = new HashSet<Claim>();
        }

        // Full Name property
        public string FullName => $"{FirstName} {LastName}";
    }
}
