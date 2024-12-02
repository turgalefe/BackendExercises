using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public interface ICustomerFactory
    {
        Task<Customer> CreateCustomerAsync(string name, string email,string password);
    }
}
