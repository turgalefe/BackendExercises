using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.ViewModels
{
    public class PostStudentViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }



    }
}
