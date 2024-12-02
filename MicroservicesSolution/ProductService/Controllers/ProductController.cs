using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 1500 },
        new Product { Id = 2, Name = "Phone", Price = 800 }
    };

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = Products.Find(p => p.Id == id);
        return product != null ? Ok(product) : NotFound("Product not found.");
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
