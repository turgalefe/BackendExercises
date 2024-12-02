using ECommerce.DataAccess;
using ECommerce.Factorymethod;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ECommerceContext _context;


        public ProductController(ProductService productService, ECommerceContext context)
        {
            _productService = productService;
            _context = context;

        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) 
                return NotFound();
            return Ok(product);
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProductPriceByName(string name)
        {
            // Ürün adı ile veritabanında arama yapıyoruz
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

            if (product == null)
            {
                return NotFound(new { Message = $"Ürün adı {name} bulunamadı." });
            }

            // Ürünün ID'sini ve fiyatını döndürüyoruz
            return Ok(new
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price
            });
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto, [FromServices] IValidator<ProductDto> validator)
        {
            // Validate the DTO
            var validationResult = await validator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Map ProductDto to Product entity
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };

            // Create the product using the service
            var newProduct = await _productService.CreateProductAsync(product.Name, product.Price);

            // Return the created product, use DTO for response if needed
            return Ok(new { Product = newProduct }); ;
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product, [FromServices] IValidator<Product> validator)
        {
            // Validate the product
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Check if the product with the specified ID exists
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            // Update product properties
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            // Update the product in the database
            await _productService.UpdateProductAsync(existingProduct);

            // Return success response with message and updated product info
            return Ok(new
            {
                Message = $"Ürün adı {existingProduct.Name} ve {existingProduct.Price} başarıyla değiştirildi.",
                UpdatedProduct = existingProduct
            });
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Check if the product with the specified ID exists
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            // Delete the product
            await _productService.DeleteProductAsync(id);

            // Return success response with message and status code 200 OK
            return Ok(new
            {
                Message = $"Ürün adı {existingProduct.Name} başarıyla silindi."
            });
        }

    }

}
