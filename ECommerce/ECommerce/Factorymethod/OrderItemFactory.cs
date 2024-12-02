using ECommerce.DataAccess;
using System;

namespace ECommerce.Factorymethod
{
    public class OrderItemFactory : IOrderItemFactory
    {
        private readonly ECommerceContext _context;

        public OrderItemFactory(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> CreateOrderItemAsync(int orderId, int productId, int quantity, decimal price)
        {
            // Ürün ve siparişin var olup olmadığını kontrol et
            var order = await _context.Orders.FindAsync(orderId);
            var product = await _context.Products.FindAsync(productId);

            if (order == null)
            {
                throw new ArgumentException("Invalid order ID.");
            }

            if (product == null)
            {
                throw new ArgumentException("Invalid product ID.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            // OrderItem oluşturma işlemi
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }
    }

}
