namespace ECommerce.DTOs
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; } // Include the OrderId
    }
}
