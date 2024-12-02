using ECommerce.DataAccess;
using System;
using System.Threading.Tasks;

namespace ECommerce.Factorymethod
{
    public class OrderFactory : IOrderFactory
    {
        private readonly ECommerceContext _context;

        public OrderFactory(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(string orderDate, decimal totalAmount, int customerId)
        {
            // Toplam tutar 0'dan küçük veya eşit olamaz
            if (totalAmount <= 0)
            {
                throw new ArgumentException("Total amount must be greater than zero.");
            }

            // Sipariş tarihi boş olamaz
            if (string.IsNullOrEmpty(orderDate))
            {
                throw new ArgumentException("Order date is required.");
            }

            // Müşteri ID'si negatif olamaz
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer ID must be greater than zero.");
            }

            // Sipariş oluşturma işlemi
            var order = new Order
            {
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                CustomerId = customerId
                // Diğer varsayılan değerler eklenebilir
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
