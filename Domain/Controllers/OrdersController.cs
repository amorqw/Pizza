using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrders _ordersService;

        public OrdersController(IOrders ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            var order = await _ordersService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersByUserId(int userId)
        {
            var orders = await _ordersService.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Orders order)
        {
            var result = await _ordersService.AddOrder(order);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, Orders order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var result = await _ordersService.UpdateOrder(order);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _ordersService.DeleteOrder(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}