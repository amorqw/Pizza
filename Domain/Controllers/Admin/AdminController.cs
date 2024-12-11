using Core.Dto.Pizza;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly PizzaService _pizzaRepository;

        public AdminController(PizzaService pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }
        
        public IActionResult Index()
        {
            return View("~/Views/Admin/AdminPanel.cshtml");
        }

        


        public IActionResult ManageIngredients()
        {
            return View("~/Views/Admin/ManageIngredients.cshtml");
        }

        public IActionResult ManageUsers()
        {
            return View("~/Views/Admin/ManageUsers.cshtml");
        }

        public IActionResult ManageOrders()
        {
            return View("~/Views/Admin/ManageOrders.cshtml");
        }
        
    }
}