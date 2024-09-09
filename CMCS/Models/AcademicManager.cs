using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class AcademicManager
    {
        [Key]
        public int ManagerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "AcademicManager";

        public ICollection<Claim> Claims { get; set; }
    }
}
