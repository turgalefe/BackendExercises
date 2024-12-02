using ECommerce.DataAccess;
using System;

namespace ECommerce.Factorymethod
{
    public class ProductFactory : IProductFactory
    {
        private readonly ECommerceContext _context;

        public ProductFactory(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(string name, decimal price)
        {
            // Ürün adı boş olamaz
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be empty or whitespace.");
            }

            // Ürün adı en az 3 karakter olmalıdır
            if (name.Length < 3)
            {
                throw new ArgumentException("Product name must be at least 3 characters long.");
            }

            // Ürün adı çok uzun olmamalıdır
            if (name.Length > 50)
            {
                throw new ArgumentException("Product name cannot exceed 50 characters.");
            }

            // Ürün fiyatı 0'dan küçük veya eşit olamaz
            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            // Ürün fiyatı çok yüksek olmamalıdır
            if (price > 10000)
            {
                throw new ArgumentException("Price cannot exceed 10,000.");
            }

            // Ürün oluşturma işlemi
            var product = new Product
            {
                Name = name,
                Price = price
                // Diğer varsayılan değerler eklenebilir
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }

}
