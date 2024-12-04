using Core.Dto.Pizza;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzas _pizzaService;

    public PizzaController(IPizzas pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetPizzaById(int id)
    {
        var pizza = await _pizzaService.GetPizza(id);

        if (pizza == null)
        {
            return NotFound($"Pizza with ID {id} not found.");
        }

        return Ok(pizza);
    }
    

    [HttpPost]
    public async Task<IActionResult> AddPizza([FromBody] PizzaDto newPizza)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPizza = await _pizzaService.CreatePizza(newPizza);
        return Ok(createdPizza);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePizza(int id, [FromBody] PizzaDto updatedPizza)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _pizzaService.UpdatePizza(id, updatedPizza);

        if (result == null)
        {
            return NotFound($"Pizza with ID {id} not found.");
        }

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePizza(int id)
    {
        var result = await _pizzaService.DeletePizza(id);

        if (!result)
        {
            return NotFound($"Pizza with ID {id} not found.");
        }

        return NoContent();
    }
}