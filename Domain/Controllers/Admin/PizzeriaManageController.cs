using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.Pizza;
using Core.Interfaces;
using Core.Models;

namespace Pizza.Controllers.Admin
{
    public class PizzeriaManageController : Controller
    {
        private readonly IPizzeria _pizzeriaService;

        public PizzeriaManageController(IPizzeria pizzeriaService)
        {
            _pizzeriaService = pizzeriaService;
        }

        [HttpGet]
        [Route("Admin/ManagePizzeria")]
        public async Task<IActionResult> ManagePizzeria()
        {
            var staffs = await _pizzeriaService.GetAllPizzerias();
            return View("~/Views/Admin/Pizzeria/ManagePizzeria.cshtml", staffs);
        }

        [HttpGet]
        [Route("Admin/EditPizzeria/{id}")]
        public async Task<IActionResult> EditPizzeria(int id)
        {
            
            var pizzeria = await _pizzeriaService.GetPizzeria(id);
            if (pizzeria == null)
            {
                return NotFound();
            }

            var pizzzeriaa = new Pizzeria()
            {
                PizzeriaId = pizzeria.PizzeriaId,
                Title = pizzeria.Title,
                Rating = pizzeria.Rating,
                Address = pizzeria.Address,
                CourierAmount = pizzeria.CourierAmount
                    
            };

            return View("~/Views/Admin/Pizzeria/EditPizzeria.cshtml", pizzzeriaa);
        }

        [HttpPost]
        [Route("Admin/UpdatePizzeria/{id}")]
        public async Task<IActionResult> UpdatePizzeria(Pizzeria pizzeria, int id)
        {
            if (ModelState.IsValid)
            {
                pizzeria.PizzeriaId = id; 
                var updatePizzeria = await _pizzeriaService.UpdatePizzeria( id, pizzeria);
                if (updatePizzeria != null)
                {
                    return RedirectToAction("ManagePizzeria");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update .");
                }
            }
            return View("~/Views/Admin/Pizzeria/EditPizzeria.cshtml");
        }


        [HttpGet]
        [Route("Admin/AddPizzeria")]
        public IActionResult AddPizzeria()
        {
            var pizzeria = new Pizzeria();  
            return View("~/Views/Admin/Pizzeria/AddPizzeria.cshtml"); 
        }

        [HttpPost]
        [Route("Admin/AddPizzeria")]
        public async Task<IActionResult> AddPizzeria(Pizzeria pizzeria)
        {
            if (ModelState.IsValid)
            {
                var newStaff = await _pizzeriaService.CreatePizzeria(pizzeria);
                if (newStaff != null)
                {
                    return RedirectToAction("ManagePizzeria");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add pizzeria");
                }
            }
            return View("~/Views/Admin/Pizzeria/AddPizzeria.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeletePizzeria/{id}")]
        public async Task<IActionResult> DeletePizzeria(int id)
        {
            var success = await _pizzeriaService.DeletePizzeria(id);
            if (success)
            {
                return RedirectToAction("ManagePizzeria");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete pizza");
                return RedirectToAction("ManagePizzeria");
            }
        }
    }
}