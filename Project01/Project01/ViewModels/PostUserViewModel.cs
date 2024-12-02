using System.ComponentModel.DataAnnotations;

namespace Project01.ViewModels
{
    public class PostUserViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }

        [Required]
        public string? Surname { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public int? Age { get; set; }


    }
}