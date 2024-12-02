using ECommerce.DataAccess;
using ECommerce.DTOs;
using ECommerce.Factorymethod;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // GET: api/orderitem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        // GET: api/orderitem
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Create the OrderItem entity using the DTO
            var newOrderItem = await _orderItemService.CreateOrderItemAsync(
                orderItemDto.OrderId,   // Pass the OrderId from the DTO
                orderItemDto.ProductId,
                orderItemDto.Quantity,
                orderItemDto.Price
            );

            return Ok(new { OrderItem = newOrderItem });
        }

        // PUT: api/orderitem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingOrderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (existingOrderItem == null) return NotFound();

            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.Price = orderItem.Price;
            existingOrderItem.OrderId = orderItem.OrderId;
            existingOrderItem.ProductId = orderItem.ProductId;
            await _orderItemService.UpdateOrderItemAsync(existingOrderItem);

            return NoContent();
        }

        // DELETE: api/orderitem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var existingOrderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (existingOrderItem == null) return NotFound();

            await _orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }

}
