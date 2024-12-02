using ECommerce.DataAccess;
using ECommerce.Factorymethod;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto, [FromServices] IValidator<Order> validator)
        {
            // OrderDto'dan Order entity'sine dönüşüm yapıyoruz
            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                CustomerId = orderDto.CustomerId
            };

            // Order üzerinde validasyonu burada yapıyoruz
            var validationResult = await validator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Validasyon başarılıysa siparişi oluştur
            var newOrder = await _orderService.CreateOrderAsync(order.OrderDate, order.TotalAmount, order.CustomerId);

            // Yeni oluşturulan siparişi dön
            return Ok(new { Order = newOrder }); ;
        }


        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order, [FromServices] IValidator<Order> validator)
        {
            // Validasyonu kontrol et
            var validationResult = await validator.ValidateAsync(order);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Siparişi ID ile kontrol et
            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null) return NotFound();

            // Güncellemeleri mevcut siparişe uygula
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.CustomerId = order.CustomerId;

            // Siparişi güncelle
            await _orderService.UpdateOrderAsync(existingOrder);

            // Güncellenen siparişi döndür
            return Ok(existingOrder); // Ya da döndüreceğiniz özel bir DTO kullanabilirsiniz
        }



        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null) return NotFound();

            // Siparişi sil
            await _orderService.DeleteOrderAsync(id);

            // Silinen siparişi döndür
            return Ok(existingOrder); // Ya da döndüreceğiniz özel bir DTO kullanabilirsiniz
        }
    }

}