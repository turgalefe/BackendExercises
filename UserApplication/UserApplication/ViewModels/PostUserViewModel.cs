using System.ComponentModel.DataAnnotations;

namespace UserApplication.ViewModels
{
    public class PostUserViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public int? Age { get; set; }


    }
}
