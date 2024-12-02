using System.ComponentModel.DataAnnotations;

namespace UserApplication.Model
{
    public class MovieSearch
    {
        [Required]
        public string Query { get; set; }

        public bool IncludeAdult { get; set; }

        public string Language { get; set; }

        public string PrimaryReleaseYear { get; set; }

        public int Page { get; set; } = 1;

        public string Region { get; set; }

        public string Year { get; set; }
    }
}
