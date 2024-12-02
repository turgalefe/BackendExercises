using ECommerce.Business;
using ECommerce.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Factorymethod
{
    public class OrderItemService
    {
        private readonly IOrderItemFactory _orderItemFactory;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly ECommerceContext _context;


        public OrderItemService(IOrderItemFactory orderItemFactory, IRepository<OrderItem> orderItemRepository, ECommerceContext context)
        {
            _orderItemFactory = orderItemFactory;
            _orderItemRepository = orderItemRepository;
            _context = context; 
        }

        // Sipariş kalemi oluşturma (Create)
        public async Task<OrderItem> CreateOrderItemAsync(int? orderId,int? productId, int quantity, decimal price)
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            // Assuming you have a DbContext instance to save changes
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        // Sipariş kalemini ID ile getirme (Read - ById)
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _orderItemRepository.GetByIdAsync(id);
        }

        // Tüm sipariş kalemlerini getirme (Read - All)
        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _orderItemRepository.GetAllAsync();
        }

        // Sipariş kalemi güncelleme (Update)
        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            await _orderItemRepository.UpdateAsync(orderItem);
        }

        // Sipariş kalemi silme (Delete)
        public async Task DeleteOrderItemAsync(int id)
        {
            await _orderItemRepository.DeleteAsync(id);
        }
    }

}
