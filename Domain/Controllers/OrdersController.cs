using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    /*
    public class OrderController : Controller
    {
        private readonly IOrders _orderService;
        private readonly IUser _userService;

        public OrderController(IOrders orderService, IUser userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult PizzaOrder(int pizzaId, int quantity)
        {
            var userId = GetUserIdFromCookie();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Если не авторизован, переадресация на логин
            }

            // Формирование заказа
            var order = new Orders
            {
                UserId = userId.Value,
                StaffId = 0, // ID сотрудника, можно заменить на логику подбора
                Status = "Создан",
                Address = GetUserAddress(userId.Value),
                PaymentMethod = "Наличные",
                Date = DateTime.Now
            };

            var orderId = _orderService.CreateOrder(order);

            // Добавление позиции в заказ
            var orderItem = new OrderItems
            {
                OrderId = orderId,
                PizzaId = pizzaId,
                Amount = quantity
            };

            _orderService.AddOrderItem(orderItem);

            // Переадресация на страницу успешного оформления заказа
            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }
        
        private int? GetUserIdFromCookie()
        {
            if (Request.Cookies.TryGetValue("userid", out var userId))
            {
                return int.Parse(userId);
            }
            return null;
        }

        private async Task<string> GetUserAddress(int userId)
        {
            var user= await _userService.GetUser(userId);
            return 
        }
    } 
    */
}