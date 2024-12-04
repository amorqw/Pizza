using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaIngredientsController : ControllerBase
{
    private readonly IPizzaIngredients _pizzaIngredientsService;

    public PizzaIngredientsController(IPizzaIngredients pizzaIngredientsService)
    {
        _pizzaIngredientsService = pizzaIngredientsService;
    }

    [HttpGet("pizza/{pizzaId}")]
    public async Task<IActionResult> GetIngredientsByPizzaId(int pizzaId)
    {
        var ingredients = await _pizzaIngredientsService.GetIngredientsByPizzaId(pizzaId);
        if (!ingredients.Any())
        {
            return NotFound($"No ingredients found for pizza with ID {pizzaId}.");
        }
        return Ok(ingredients);
    }

    [HttpGet("ingredient/{ingredientId}")]
    public async Task<IActionResult> GetPizzasByIngredientId(int ingredientId)
    {
        var pizzas = await _pizzaIngredientsService.GetPizzasByIngredientId(ingredientId);
        if (!pizzas.Any())
        {
            return NotFound($"No pizzas found for ingredient with ID {ingredientId}.");
        }
        return Ok(pizzas);
    }

    [HttpPost]
    public async Task<IActionResult> AddPizzaIngredient([FromBody] PizzaIngredients pizzaIngredient)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _pizzaIngredientsService.AddPizzaIngredient(pizzaIngredient);

        if (!result)
        {
            return BadRequest("Failed to add the pizza-ingredient relation.");
        }

        return CreatedAtAction(nameof(GetIngredientsByPizzaId), new { pizzaId = pizzaIngredient.PizzaId }, pizzaIngredient);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePizzaIngredient(int pizzaId, int ingredientId)
    {
        var result = await _pizzaIngredientsService.DeletePizzaIngredient(pizzaId, ingredientId);

        if (!result)
        {
            return NotFound($"Relation between pizza ID {pizzaId} and ingredient ID {ingredientId} not found.");
        }

        return NoContent();
    }
}
