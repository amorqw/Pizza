using Core.Dto.Order;
using Core.Dto.Review;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrders _ordersService;
        private readonly IOrderItems _orderItemsService;
        private readonly IReviews _reviewsService;

        public OrderController(IOrders ordersService, IOrderItems orderItemsService, IReviews reviewsService)
        {
            _ordersService = ordersService;
            _orderItemsService = orderItemsService;
            _reviewsService = reviewsService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string address, string paymentMethod)
        {
            Console.WriteLine("Submit order action started");

            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order") ??
                        new List<(int, int)>();

            if (order == null || !order.Any())
            {
                Console.WriteLine("Order is empty or null");
                return RedirectToAction("Menu", "PizzeriaPizza");
            }

            var staffId = 1;
            var newOrder = new OrderDto()
            {
                UserId = userId,
                StaffId = staffId,
                Date = DateTime.Now,
                Status = "В обработке",
                Address = address,
                PaymentMethod = paymentMethod
            };

            var orderCreated = await _ordersService.AddOrderReturnId(newOrder);

            if (orderCreated != null)
            {
                var orderId = orderCreated.Value;

                // Сохраняем OrderId в сессии
                HttpContext.Session.SetInt32("OrderId", orderId);

                Console.WriteLine($"Order created with OrderId: {orderId}");

                foreach (var item in order)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = orderId,
                        PizzaId = item.PizzaId,
                        Amount = item.Quantity
                    };
                    await _orderItemsService.CreateOrderItem(orderItem);

                    Console.WriteLine($"Order item added: PizzaId = {item.PizzaId}, Quantity = {item.Quantity}");
                }

                return View("~/Views/OrderConfirmation.cshtml");
            }

            Console.WriteLine("Failed to create order");
            return View("Error");
        }

        [HttpGet]
        public IActionResult LeaveReview()
        {
            var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order");
            if (order == null || !order.Any())
            {
                Console.WriteLine("Order is empty or null for review");
                return RedirectToAction("Menu", "PizzeriaPizza");
            }

            Console.WriteLine("Leave review view displayed");
            return View("~/Views/LeaveReview.cshtml", order);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(int rating, string comment)
        {
            Console.WriteLine("Submit review action started");

            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order");
            if (order == null || !order.Any())
            {
                Console.WriteLine("Order is empty or null for review submission");
                return RedirectToAction("Menu", "PizzeriaPizza");
            }

            var orderItem = order.FirstOrDefault();
            if (orderItem.Equals(default((int PizzaId, int Quantity))))
            {
                Console.WriteLine("No valid order item found for review submission");
                return View("Error");
            }

            // Используем правильный OrderId, который был сохранен в сессии
            var orderId = HttpContext.Session.GetInt32("OrderId") ?? 0;

            // Проверка, существует ли заказ с данным OrderId
            var orderExists = await _ordersService.GetOrderById(orderId) != null;
            if (!orderExists)
            {
                Console.WriteLine($"Order with OrderId {orderId} does not exist");
                return View("Error");
            }

            var reviewDto = new ReviewDto
            {
                OrderId = orderId,  // Используем правильный OrderId
                PizzaId = orderItem.PizzaId,
                UserId = userId,
                Rating = rating,
                Comment = comment
            };

            var result = await _reviewsService.AddReview(reviewDto);
            if (result)
            {
                Console.WriteLine("Review submitted successfully");
                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Failed to submit review");
            return View("~/Views/Home/Home.cshtml");
        }
    }
}
