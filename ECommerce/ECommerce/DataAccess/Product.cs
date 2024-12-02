using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.DataAccess
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; } // Primary Key olarak işaretlenmiştir

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        // 1-N ilişkiyi temsil eder
        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
