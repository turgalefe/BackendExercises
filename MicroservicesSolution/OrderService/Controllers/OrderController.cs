using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private static readonly List<Order> Orders = new List<Order>();
    private readonly HttpClient _httpClient;

    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(int productId, int quantity)
    {
        var productResponse = await _httpClient.GetAsync($"http://localhost:5000/api/gateway/product/{productId}");

        if (!productResponse.IsSuccessStatusCode)
            return BadRequest("Product not found.");

        var product = JsonSerializer.Deserialize<Product>(await productResponse.Content.ReadAsStringAsync());
        var order = new Order
        {
            Id = Orders.Count + 1,
            ProductId = productId,
            Quantity = quantity,
            TotalPrice = product.Price * quantity
        };

        Orders.Add(order);
        return Ok(order);
    }
}

public class Order
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
