using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public interface IProductFactory
    {
        Task<Product> CreateProductAsync(string name, decimal price);
    }

}
