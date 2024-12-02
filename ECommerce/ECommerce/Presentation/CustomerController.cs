using ECommerce.Business;
using ECommerce.DataAccess;
using ECommerce.Factorymethod;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IConfiguration _configuration;

        public CustomerController(CustomerService customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // GET: api/customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto, [FromServices] IValidator<CustomerDto> validator)
        {
            // DTO'yu doğrula
            var validationResult = await validator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // CustomerDto'dan Customer nesnesi oluştur
            var newCustomer = await _customerService.CreateCustomerAsync(customerDto.Name, customerDto.Email, customerDto.Password);

            // Başarılı bir yanıt döndür
            return Ok(new { Customer = newCustomer });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer, [FromServices] IValidator<Customer> validator)
        {
            // Validate customer
            var validationResult = await validator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Check if the customer exists
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return NotFound();

            // Update customer details
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            await _customerService.UpdateCustomerAsync(existingCustomer);

            return Ok(); // 200 Success if the update is successful
        }



        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            // Müşteriyi buluyoruz
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null) return NotFound();

            // Müşteriyi siliyoruz
            await _customerService.DeleteCustomerAsync(id);

            // Silinen müşteri bilgilerini döndürüyoruz
            return Ok(new
            {
                CustomerId = existingCustomer.Id,
                CustomerName = existingCustomer.Name,
                CustomerEmail = existingCustomer.Email,
                // Diğer gerekli müşteri özelliklerini burada ekleyebilirsin
            });
        }
    }

}
