using System.ComponentModel.DataAnnotations;

namespace CrudBackend.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? Phone { get; set; }
    }
}
