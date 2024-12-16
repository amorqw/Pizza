using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers.Admin;

public class PizzaAvailableController: Controller
{
    private readonly IPizzasAvailable _pizzasAvailableService;

    public PizzaAvailableController(IPizzasAvailable pizzasAvailableService)
    {
        _pizzasAvailableService = pizzasAvailableService;
    }
    [HttpGet]
    [Route("Admin/ManagePizzaAvailable")]
    public async Task<IActionResult> ManagePizzaAvailable()
    {
        var pizzasAvailable = await _pizzasAvailableService.GetAll();
        return View("~/Views/Admin/PizzaAvailable/ManagePizzaAvailable.cshtml", pizzasAvailable);
    }

    [HttpGet]
    [Route("Admin/EditPizzasAvailable")]
    public async Task<IActionResult> Edit([FromQuery] int pizzeriaId, [FromQuery] int pizzaId)
    {
        var pizzasAvailable = await _pizzasAvailableService.GetById(pizzeriaId, pizzaId);
        if (pizzasAvailable == null)
        {
            return NotFound();
        }
        return View("~/Views/Admin/PizzaAvailable/EditPizzaAvailable.cshtml", pizzasAvailable);
    }
    [HttpPost]
    [Route("Admin/UpdatePizzasAvailable")]
    public async Task<IActionResult> Update(PizzasAvailable pizzasAvailable)
    {
        if (ModelState.IsValid)
        {
            var success = await _pizzasAvailableService.Update(pizzasAvailable);
            if (success)
            {
                return RedirectToAction("ManagePizzaAvailable");
            }
        }
        ModelState.AddModelError(string.Empty, "Failed to update record.");
        return View("~/Views/Admin/PizzaAvailable/EditPizzaAvailable.cshtml", pizzasAvailable);
    }
    [HttpPost]
    [Route("Admin/DeletePizzasAvailable")]
    public async Task<IActionResult> Delete([FromQuery] int pizzeriaId, [FromQuery] int pizzaId)
    {
        var success = await _pizzasAvailableService.Delete(pizzeriaId, pizzaId);
        if (success)
        {
            return RedirectToAction("ManagePizzaAvailable");
        }
        ModelState.AddModelError(string.Empty, "Failed to delete record.");
        return RedirectToAction("ManagePizzaAvailable");
    }

    [HttpGet]
    [Route("Admin/AddPizzaAvailable")]
    public IActionResult Add()
    {
        return View("~/Views/Admin/PizzaAvailable/AddPizzaAvailable.cshtml");
    }
    [HttpPost]
    [Route("Admin/AddPizzaAvailable")]
    public async Task<IActionResult> Add(PizzasAvailable pizzasAvailable)
    {
        if (ModelState.IsValid)
        {
            var success = await _pizzasAvailableService.Add(pizzasAvailable);
            if (success)
            {
                return RedirectToAction("ManagePizzaAvailable");
            }
            ModelState.AddModelError(string.Empty, "Failed to add record.");
        }
        return View("~/Views/Admin/PizzaAvailable/AddPizzaAvailable.cshtml", pizzasAvailable);
    }
    
}