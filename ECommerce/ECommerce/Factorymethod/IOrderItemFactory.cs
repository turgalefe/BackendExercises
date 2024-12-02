using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public interface IOrderItemFactory
    {
        Task<OrderItem> CreateOrderItemAsync(int orderId, int productId, int quantity, decimal price);
    }

}
