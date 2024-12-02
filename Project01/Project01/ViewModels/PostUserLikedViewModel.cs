using System.ComponentModel.DataAnnotations;

namespace Project01.ViewModels
{
    public class PostUserLikedViewModel
    {
        [Required]
        public bool IsLiked { get; set; }

        [Required]
        public int? Ratings { get; set; }

        [Required]
        public string? Comments { get; set; }
    }
}
