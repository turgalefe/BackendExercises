using ECommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Business
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly ECommerceContext _context;

        public OrderItemRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order) // İlişkili Order'ı dahil et
                .Include(oi => oi.Product) // İlişkili Product'ı dahil et
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .ToListAsync();
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
