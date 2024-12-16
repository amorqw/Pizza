using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers.Admin;

public class PizzeriaPizzaController : Controller
{
    private readonly IPizzasAvailable _pizzasAvailableService;
    private readonly IPizzeria _pizzeriaService;
    private readonly IPizzas _pizzasService;

    public PizzeriaPizzaController(IPizzasAvailable pizzasAvailableService, IPizzeria pizzeriaService, IPizzas pizzasService)
    {
        _pizzasAvailableService = pizzasAvailableService;
        _pizzeriaService = pizzeriaService;
        _pizzasService = pizzasService;
    }

    public async Task<IActionResult> Menu(int pizzeriaId)
    {
        // Получаем пиццерию по ID
        var pizzeria = await _pizzeriaService.GetPizzeria(pizzeriaId);

        var pizzasAvailable = await _pizzasAvailableService.GetAll();  // Возвращает все доступные пиццы
        var availablePizzas = pizzasAvailable.Where(pa => pa.PizzeriaId == pizzeriaId && pa.Available).ToList();

        var pizzas = new List<Pizzas>();
        foreach (var pizzaAvailable in availablePizzas)
        {
            var pizza = await _pizzasService.GetPizza(pizzaAvailable.PizzaId); // Получаем пиццу по ID
            pizzas.Add(pizza);
        }

        ViewBag.Pizzeria = pizzeria;  
        ViewBag.Pizzas = pizzas;      

        return View("~/Views/Menu.cshtml");
    }

}