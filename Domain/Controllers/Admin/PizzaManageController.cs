using Microsoft.AspNetCore.Mvc;
using Core.Dto.Pizza;
using Core.Interfaces;
using Core.Models;
using System.Threading.Tasks;

namespace Pizza.Controllers.Admin
{
    public class PizzaManageController : Controller
    {
        private readonly IPizzas _pizzaService;

        public PizzaManageController(IPizzas pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        [Route("Admin/ManagePizzas")]
        public async Task<IActionResult> ManagePizzas()
        {
            var pizzas = await _pizzaService.GetAllPizzasAsync();
            return View("~/Views/Admin/Pizza/ManagePizzas.cshtml", pizzas);
        }

        [HttpGet]
        [Route("Admin/EditPizza/{id}")]
        public async Task<IActionResult> EditPizza(int id)
        {
            var pizza = await _pizzaService.GetPizza(id);
            if (pizza == null)
            {
                return NotFound();
            }

            var pizzaDto = new PizzaDto
            {
                PizzaId = pizza.PizzaId,
                NamePizza = pizza.NamePizza,
                Description = pizza.Description,
                Price = pizza.Price,
                Size = pizza.Size,
                Available = pizza.Available
            };

            return View("~/Views/Admin/Pizza/EditPizza.cshtml", pizzaDto);
        }

        [HttpPost]
        [Route("Admin/UpdatePizza/{id}")]
        public async Task<IActionResult> UpdatePizza(PizzaDto pizzaDto, int id)
        {
            if (ModelState.IsValid)
            {
                var updatedPizza = await _pizzaService.UpdatePizza(id, pizzaDto);
                if (updatedPizza != null)
                {
                    return RedirectToAction("ManagePizzas");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update pizza.");
                }
            }
            return View("~/Views/Admin/Pizza/EditPizza.cshtml", pizzaDto);
        }

        [HttpGet]
        [Route("Admin/AddPizza")]
        public IActionResult AddPizza()
        {
            var pizzaDto = new PizzaDto();  
            return View("~/Views/Admin/Pizza/AddPizza.cshtml"); 
        }

        [HttpPost]
        [Route("Admin/AddPizza")]
        public async Task<IActionResult> AddPizza(PizzaDto pizzaDto)
        {
            if (ModelState.IsValid)
            {
                var newPizza = await _pizzaService.CreatePizza(pizzaDto);
                if (newPizza != null)
                {
                    return RedirectToAction("ManagePizzas");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add pizza.");
                }
            }
            return View("~/Views/Admin/Pizza/AddPizza.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeletePizza/{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var success = await _pizzaService.DeletePizza(id);
            if (success)
            {
                return RedirectToAction("ManagePizzas");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete pizza.");
                return RedirectToAction("ManagePizzas");
            }
        }
    }
}
