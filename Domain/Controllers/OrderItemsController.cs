using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItems _orderItemsService;

        public OrderItemsController(IOrderItems orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItems>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemsService.GetOrderItemById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItems>>> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = await _orderItemsService.GetOrderItemsByOrderId(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderItem(OrderItems orderItem)
        {
            var result = await _orderItemsService.AddOrderItem(orderItem);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.OrderItemId }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderItem(int id, OrderItems orderItem)
        {
            if (id != orderItem.OrderItemId)
            {
                return BadRequest();
            }

            var result = await _orderItemsService.UpdateOrderItem(orderItem);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemsService.DeleteOrderItem(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}