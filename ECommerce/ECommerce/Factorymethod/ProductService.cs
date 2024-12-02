using ECommerce.Business;
using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public class ProductService
    {
        private readonly IProductFactory _productFactory;
        private readonly IRepository<Product> _productRepository;

        public ProductService(IProductFactory productFactory, IRepository<Product> productRepository)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
        }

        // Ürün oluşturma (Create)
        public async Task<Product> CreateProductAsync(string name, decimal price)
        {
            return await _productFactory.CreateProductAsync(name, price);
        }

        // Ürünü ID ile getirme (Read - ById)
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        // Tüm ürünleri getirme (Read - All)
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        // Ürün güncelleme (Update)
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        // Ürün silme (Delete)
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }

}
