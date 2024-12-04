using Core.Dto.Ingredients;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredients _ingredientsService;

    public IngredientsController(IIngredients ingredientsService)
    {
        _ingredientsService = ingredientsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngrDto ingredient)
    {
        var ingr= await _ingredientsService.CreateIngredient(ingredient);
        return Ok(ingr);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        var res= await _ingredientsService.DeleteIngredient(id);
        return Ok(res);
    }
}