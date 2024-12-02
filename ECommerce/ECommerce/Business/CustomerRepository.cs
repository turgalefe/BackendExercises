using ECommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Business
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ECommerceContext _context;

        public CustomerRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Orders) // İlişkili Order'ları dahil et
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }

}
