using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerce.DataAccess
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Make OrderId nullable
        public int? OrderId { get; set; } // Foreign Key (Order)

        [JsonIgnore]
        public Order? Order { get; set; } // Order with N-1 relationship

        public int? ProductId { get; set; } // Foreign Key (Product)

        [JsonIgnore]
        public Product? Product { get; set; } // Product with N-1 relationship
    }
}
