using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.DataAccess
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // This line is important
        public int Id { get; set; } // Assuming this is the primary key

        [Required]
        public string OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; } // Foreign Key olarak işaretlenmiştir

        [JsonIgnore]
        public Customer? Customer { get; set; } // Customer ile 1-N ilişki

        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; } // 1-N ilişkiyi temsil eder
    }
}