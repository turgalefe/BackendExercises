using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public interface IOrderFactory
    {
        Task<Order> CreateOrderAsync(string orderDate, decimal totalAmount, int customerId);
    }

}
