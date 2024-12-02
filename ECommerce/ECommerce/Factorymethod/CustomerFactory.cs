using ECommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce.Factorymethod
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ECommerceContext _context;

        public CustomerFactory(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomerAsync(string name, string email, string password)
        {
            // E-posta formatı kontrolü
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (await _context.Customers.AnyAsync(c => c.Email == email))
            {
                throw new ArgumentException("Email already exists.");
            }

            // İsim kontrolü
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty or whitespace.");
            }

            if (name.Length < 2)
            {
                throw new ArgumentException("Name must be at least 2 characters long.");
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("Name cannot exceed 50 characters.");
            }

            // Şifre kontrolü
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty.");
            }

            if (password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            if (password.Length > 100)
            {
                throw new ArgumentException("Password cannot exceed 100 characters.");
            }

            // Her şey doğruysa müşteri oluşturma işlemi
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Password = password // Şifreyi doğru bir şekilde saklamak için hashleme yapılmalıdır!
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
