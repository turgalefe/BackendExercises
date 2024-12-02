namespace ECommerce.DataAccess
{
    public class ProductDto
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Ensure OrderItems is not included if you don't need it
        // [JsonIgnore] is not needed here if it's not part of the DTO
    }
}
