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
        private readonly IStaff _staffService;

        public OrderController(IOrders ordersService, IOrderItems orderItemsService, IReviews reviewsService, IStaff staffService)
        {
            _ordersService = ordersService;
            _orderItemsService = orderItemsService;
            _reviewsService = reviewsService;
            _staffService = staffService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit(string address, string paymentMethod)
        {

            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order") ??
                        new List<(int, int)>();

            if (order == null || !order.Any())
            {
                return RedirectToAction("Menu", "PizzeriaPizza");
            }

            var random = new Random();
            var staffList = await _staffService.GetAllStaff();
            var randomStaff = staffList.ElementAt(random.Next(staffList.Count()));
            
            var staffId = randomStaff.StaffId;
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
                //
                HttpContext.Session.SetInt32("OrderId", orderId);

                foreach (var item in order)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = orderId,
                        PizzaId = item.PizzaId,
                        Amount = item.Quantity
                    };
                    await _orderItemsService.CreateOrderItem(orderItem);

                }

                return View("~/Views/OrderConfirmation.cshtml");
            }

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
                return RedirectToAction("Menu", "PizzeriaPizza");
            }

            var orderItem = order.FirstOrDefault();
            if (orderItem.Equals(default((int PizzaId, int Quantity))))
            {
                return View("Error");
            }

            var orderId = HttpContext.Session.GetInt32("OrderId") ?? 0;

            var orderExists = await _ordersService.GetOrderById(orderId) != null;
            if (!orderExists)
            {
                Console.WriteLine($"Order with OrderId {orderId} does not exist");
                return View("Error");
            }

            var reviewDto = new ReviewDto
            {
                OrderId = orderId,  
                PizzaId = orderItem.PizzaId,
                UserId = userId,
                Rating = rating,
                Comment = comment
            };

            var result = await _reviewsService.AddReview(reviewDto);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Home.cshtml");
        }
    }
}
