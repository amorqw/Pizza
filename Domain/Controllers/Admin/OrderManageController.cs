using Core.Dto.Order;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers.Admin
{
    public class OrderManageController : Controller
    {
        private readonly IOrders _ordersService;

        public OrderManageController(IOrders ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("Admin/ManageOrders")]
        public async Task<IActionResult> ManageOrders()
        {
            var orders = await _ordersService.GetAllOrders();
            return View("~/Views/Admin/Order/ManageOrders.cshtml", orders);
        }

        [HttpGet]
        [Route("Admin/EditOrder")]
        public async Task<IActionResult> EditOrder([FromQuery] int orderId)
        {
            var order = await _ordersService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return View("~/Views/Admin/Order/EditOrder.cshtml", order);
        }

        [HttpPost]
        [Route("Admin/UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderDto order)
        {
            if (ModelState.IsValid)
            {
                var success = await _ordersService.UpdateOrder(order);
                if (success)
                {
                    return RedirectToAction("ManageOrders");
                }
                
            }
            ModelState.AddModelError(string.Empty, "Failed to update order.");
            return View("~/Views/Admin/Order/EditOrder.cshtml", order);
        }

        [HttpPost]
        [Route("Admin/DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery] int orderId)
        {
            var success = await _ordersService.DeleteOrder(orderId);
            if (success)
            {
                return RedirectToAction("ManageOrders");
            }
            ModelState.AddModelError(string.Empty, "Failed to delete order.");
            return RedirectToAction("ManageOrders");
        }

        [HttpGet]
        [Route("Admin/AddOrder")]
        public IActionResult AddOrder()
        {
            return View("~/Views/Admin/Order/AddOrder.cshtml");
        }

        [HttpPost]
        [Route("Admin/AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto order)
        {
            if (ModelState.IsValid)
            {
                var success = await _ordersService.AddOrder(order);
                if (success)
                {
                    return RedirectToAction("ManageOrders");
                }
                ModelState.AddModelError(string.Empty, "Failed to add order.");
            }
            return View("~/Views/Admin/Order/AddOrder.cshtml", order);
        }
    }
}
