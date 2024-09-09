using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Coordinator
    {
        [Key]
        public int CoordinatorID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } = "Coordinator";
        // Add the missing Password property
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Navigation property for Claims (claims assigned to this coordinator)
        public ICollection<Claim> Claims { get; set; }
    }
}
