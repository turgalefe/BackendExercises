namespace ECommerce.DataAccess
{
    public class OrderDto
    {
        public string OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
}
