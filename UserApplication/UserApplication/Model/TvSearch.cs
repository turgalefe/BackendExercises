using System.ComponentModel.DataAnnotations;

namespace UserApplication.Model
{
    public class TvSearch
    {
        [Required]
        public string Query { get; set; }

        public bool IncludeAdult { get; set; }
        public string Language { get; set; }

        public int first_air_date_year { get; set; }

        public int Page { get; set; } = 1;

        public string Year { get; set; }
    }
}
