using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models;

namespace Pizza.Controllers.Admin
{
    public class OrderItemsController : Controller
    {
        private readonly IOrderItems _orderItemsService;

        public OrderItemsController(IOrderItems orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        [HttpGet]
        [Route("Admin/ManageOrderItems")]
        public async Task<IActionResult> ManageOrderItems()
        {
            var orderItems = await _orderItemsService.GetAllOrderItemsAsync();
            return View("~/Views/Admin/OrderItems/ManageOrderItems.cshtml", orderItems);
        }

        [HttpGet]
        [Route("Admin/EditOrderItem")]
        public async Task<IActionResult> EditOrderItem([FromQuery]int  orderId, [FromQuery]int pizzaId)
        {
            var orderItem = await _orderItemsService.GetOrderItem(orderId, pizzaId);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/OrderItems/EditOrderItems.cshtml", orderItem);
        }

        [HttpPost]
        [Route("Admin/UpdateOrderItem")]
        public async Task<IActionResult> UpdateOrderItem(OrderItems orderItem, int OrderId, int PizzaId)
        {
            if (ModelState.IsValid)
            {
                var updatedOrderItem = await _orderItemsService.UpdateOrderItem(OrderId, PizzaId, orderItem);
                if (updatedOrderItem != null)
                {
                    return RedirectToAction("ManageOrderItems");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update order item.");
                }
            }
            return View("~/Views/Admin/OrderItems/EditOrderItems.cshtml", orderItem);
        }


        [HttpGet]
        [Route("Admin/AddOrderItem")]
        public IActionResult AddOrderItem()
        {
            return View("~/Views/Admin/OrderItems/AddOrderItems.cshtml");
        }

        [HttpPost]
        [Route("Admin/AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(OrderItems orderItem)
        {
            if (ModelState.IsValid)
            {
                var newOrderItem = await _orderItemsService.CreateOrderItem(orderItem);
                if (newOrderItem != null)
                {
                    return RedirectToAction("ManageOrderItems");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add order item.");
                }
            }
            return View("~/Views/Admin/OrderItems/AddOrderItems.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeleteOrderItem")]
        public async Task<IActionResult> DeleteOrderItem([FromQuery]int orderId, [FromQuery]int pizzaId)
        {
            var success = await _orderItemsService.DeleteOrderItem(orderId, pizzaId);
            if (success)
            {
                return RedirectToAction("ManageOrderItems");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete order item.");
                return RedirectToAction("ManageOrderItems");
            }
        }
    }
}
