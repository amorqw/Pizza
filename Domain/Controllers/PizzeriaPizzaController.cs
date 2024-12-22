using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

public class PizzeriaPizzaController : Controller
{
    private readonly IPizzasAvailable _pizzasAvailableService;
    private readonly IPizzeria _pizzeriaService;
    private readonly IPizzas _pizzasService;

    public PizzeriaPizzaController(IPizzasAvailable pizzasAvailableService, IPizzeria pizzeriaService,
        IPizzas pizzasService)
    {
        _pizzasAvailableService = pizzasAvailableService;
        _pizzeriaService = pizzeriaService;
        _pizzasService = pizzasService;
    }

    public async Task<IActionResult> Menu(int pizzeriaId, string orderBy = "Price", string sortDirection = "ASC", string size = null)
    {
        var pizzeria = await _pizzeriaService.GetPizzeria(pizzeriaId);

        var pizzasAvailable = await _pizzasAvailableService.GetAll();
        var availablePizzas = pizzasAvailable.Where(pa => pa.PizzeriaId == pizzeriaId && pa.Available).ToList();

        var pizzas = new List<Pizzas>();
        foreach (var pizzaAvailable in availablePizzas)
        {
            var pizza = await _pizzasService.GetPizza(pizzaAvailable.PizzaId);
            pizzas.Add(pizza);
        }

        pizzas = pizzas.Where(p => string.IsNullOrEmpty(size) || p.Size.Equals(size, StringComparison.OrdinalIgnoreCase))
            .OrderBy(p => orderBy == "Name" ? p.Title : p.Price.ToString())
            .ToList();

        if (sortDirection.ToUpper() == "DESC")
        {
            pizzas.Reverse();
        }

        ViewBag.Pizzeria = pizzeria;
        ViewBag.Pizzas = pizzas;

        return View("~/Views/Menu.cshtml");
    }


    public IActionResult AddToOrder(int pizzaId, int quantity, int pizzeriaId)
    {
        var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order") ?? new List<(int, int)>();

        var existingPizza = order.FirstOrDefault(o => o.PizzaId == pizzaId);
        if (existingPizza.PizzaId != 0)
        {
            order.Remove(existingPizza);
            order.Add((pizzaId, existingPizza.Quantity + quantity));
        }
        else
        {
            order.Add((pizzaId, quantity));
        }

        HttpContext.Session.SetObject("Order", order);

        return RedirectToAction("Menu", new { pizzeriaId = pizzeriaId });
    }




    public async Task<IActionResult> Checkout()
    {
        var order = HttpContext.Session.GetObject<List<(int PizzaId, int Quantity)>>("Order");
        if (order == null || !order.Any())
        {
            return RedirectToAction("Menu", new { pizzeriaId = Request.Query["pizzeriaId"] });
        }
        
        var pizzasWithDetails = new List<(Pizzas Pizza, int Quantity)>();
        foreach (var item in order)
        {
            var pizza = await _pizzasService.GetPizza(item.PizzaId); 
            if (pizza != null)
            {
                pizzasWithDetails.Add((pizza, item.Quantity));
            }
        }


        ViewBag.Order = pizzasWithDetails;
        return View("~/Views/Checkout.cshtml");
    }


}