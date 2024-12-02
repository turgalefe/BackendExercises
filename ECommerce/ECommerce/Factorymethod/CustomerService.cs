using ECommerce.Business;
using ECommerce.DataAccess;

namespace ECommerce.Factorymethod
{
    public class CustomerService
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(ICustomerFactory customerFactory, IRepository<Customer> customerRepository)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
        }

        // Müşteri oluşturma (Create)
        public async Task<Customer> CreateCustomerAsync(string name, string email, string password)
        {
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Password = password  // Parola alanını ekliyoruz
            };

            await _customerRepository.AddAsync(customer);
            return customer;
        }

        // Müşteriyi ID ile getirme (Read - ById)
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        // Tüm müşterileri getirme (Read - All)
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        // Müşteri güncelleme (Update)
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        // Müşteri silme (Delete)
        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }

}
