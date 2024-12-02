using ECommerce.Business;
using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public class OrderService
    {
        private readonly IOrderFactory _orderFactory;
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IOrderFactory orderFactory, IRepository<Order> orderRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(string orderDate, decimal totalAmount, int customerId)
        {
            // Factory ile sipariş oluştur
            return await _orderFactory.CreateOrderAsync(orderDate, totalAmount, customerId);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            // Repository ile belirli bir siparişi getir
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            // Repository ile tüm siparişleri getir
            return await _orderRepository.GetAllAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Siparişi güncelle
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            // Siparişi sil
            await _orderRepository.DeleteAsync(id);
        }

        //internal async Task CreateOrderAsync(string orderDate, decimal totalAmount, int customerId)
        //{
        //    throw new NotImplementedException();
        //}
    }

}