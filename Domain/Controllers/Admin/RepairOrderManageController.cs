using Core.Dto.RepairOrder;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pizza.Controllers.Admin
{
    [Authorize]
    public class RepairOrderManageController : Controller
    {
        private readonly IRepairOrderService _repairOrderService;

        public RepairOrderManageController(IRepairOrderService repairOrderService)
        {
            _repairOrderService = repairOrderService;
        }

        [HttpGet]
        [Route("Admin/ManageRepairOrders")]
        public async Task<IActionResult> ManageRepairOrders()
        {
            var orders = await _repairOrderService.GetAllOrders();
            return View("~/Views/Admin/RepairOrder/ManageRepairOrders.cshtml", orders);
        }

        [HttpGet]
        [Route("Admin/AddRepairOrder")]
        public async Task<IActionResult> AddRepairOrder()
        {
            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;
            return View("~/Views/Admin/RepairOrder/AddRepairOrder.cshtml", new RepairOrderDto());
        }

        [HttpPost]
        [Route("Admin/AddRepairOrder")]
        public async Task<IActionResult> AddRepairOrder(RepairOrderDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Guid.TryParse(Request.Form["ServiceId"].ToString(), out Guid serviceId))
                    {
                        dto.ServiceId = serviceId;
                        var order = await _repairOrderService.CreateRepairOrder(dto, Guid.Empty);
                        if (order != null)
                        {
                            return RedirectToAction("ManageRepairOrders");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ServiceId", "Неверный формат ID услуги");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Не удалось создать заказ: " + ex.Message);
                }
            }
            
            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;
            return View("~/Views/Admin/RepairOrder/AddRepairOrder.cshtml", dto);
        }

        [HttpGet]
        [Route("Admin/EditRepairOrder/{id}")]
        public async Task<IActionResult> EditRepairOrder(Guid id)
        {
            var order = await _repairOrderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;
            return View("~/Views/Admin/RepairOrder/EditRepairOrder.cshtml", order);
        }

        [HttpPost]
        [Route("Admin/UpdateRepairOrder/{id}")]
        public async Task<IActionResult> UpdateRepairOrder(RepairOrderDto orderDto, Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Guid.TryParse(Request.Form["ServiceId"].ToString(), out Guid serviceId))
                    {
                        orderDto.ServiceId = serviceId;
                        var updatedOrder = await _repairOrderService.UpdateOrder(orderDto, id);
                        if (updatedOrder != null)
                        {
                            return RedirectToAction("ManageRepairOrders");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ServiceId", "Неверный формат ID услуги");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Не удалось обновить заказ: " + ex.Message);
                }
            }
            
            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;
            return View("~/Views/Admin/RepairOrder/EditRepairOrder.cshtml", orderDto);
        }

        [HttpPost]
        [Route("Admin/DeleteRepairOrder/{id}")]
        public async Task<IActionResult> DeleteRepairOrder(Guid id)
        {
            var success = await _repairOrderService.DeleteOrder(id);
            if (success)
            {
                return RedirectToAction("ManageRepairOrders");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить заказ.");
                return RedirectToAction("ManageRepairOrders");
            }
        }
    }
} 