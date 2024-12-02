using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public GatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5001/api/product/{id}");

        if (!response.IsSuccessStatusCode)
            return NotFound("Product not found.");

        var productData = await response.Content.ReadAsStringAsync();
        return Content(productData, "application/json");
    }
}
