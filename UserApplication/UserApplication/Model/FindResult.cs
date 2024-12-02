using System.ComponentModel.DataAnnotations;

namespace UserApplication.Model
{
    public class FindResult
    {
        [Required]
        public int externalId { get; set; }

        [Required]
        public string externalSource { get; set; }
        public string language { get; set; }

    }
}
