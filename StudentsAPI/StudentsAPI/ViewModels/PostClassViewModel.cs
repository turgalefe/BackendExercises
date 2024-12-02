using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.ViewModels
{
    public class PostClassViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
