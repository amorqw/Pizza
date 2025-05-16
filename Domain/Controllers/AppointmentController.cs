using Core.Dto.RepairOrder;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Pizza.Controllers;

[Authorize]
public class AppointmentController : Controller
{
    private readonly IRepairOrderService _repairOrderService;

    public AppointmentController(IRepairOrderService repairOrderService)
    {
        _repairOrderService = repairOrderService;
    }

    [HttpGet]
    public async Task<IActionResult> Book()
    {
        try
        {
            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;
            return View(new RepairOrderDto());
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ошибка загрузки списка услуг";
            return View(new RepairOrderDto());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Book(RepairOrderDto dto)
    {
        try
        {
            var services = await _repairOrderService.GetAllServices();
            ViewBag.Services = services;

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Пожалуйста, заполните все поля корректно!";
                return View(dto);
            }

            if (dto.ServiceId == Guid.Empty)
            {
                ViewBag.Error = "Пожалуйста, выберите услугу!";
                return View(dto);
            }

            if (string.IsNullOrWhiteSpace(dto.CarModel))
            {
                ViewBag.Error = "Пожалуйста, укажите модель автомобиля!";
                return View(dto);
            }

            if (string.IsNullOrWhiteSpace(dto.CarRegistrationNumber))
            {
                ViewBag.Error = "Пожалуйста, укажите государственный номер!";
                return View(dto);
            }

            if (dto.DateTime == default || dto.DateTime == DateTime.MinValue)
            {
                ViewBag.Error = "Пожалуйста, укажите дату и время!";
                return View(dto);
            }

            var userIdStr = User.FindFirstValue("userid");

            if (!Guid.TryParse(userIdStr, out var userId))
            {
                ViewBag.Error = "Ошибка идентификации пользователя.";
                return View(dto);
            }

            if (!await _repairOrderService.IsSlotAvailable(dto.DateTime))
            {
                ViewBag.Error = "Выбранное время уже занято. Пожалуйста, выберите другое время.";
                return View(dto);
            }

            var order = await _repairOrderService.CreateRepairOrder(dto, userId);
            
            if (order == null)
            {
                ViewBag.Error = "Ошибка создания заказа";
                return View(dto);
            }

            ViewBag.Success = $"Запись успешно создана! Дата: {order.DateTime:dd.MM.yyyy HH:mm}, Услуга: {services.FirstOrDefault(s => s.IdService == order.IdService)?.ServiceName}";
            return View(new RepairOrderDto());
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ошибка создания заказа. Пожалуйста, попробуйте позже.";
            return View(dto);
        }
    }
} 