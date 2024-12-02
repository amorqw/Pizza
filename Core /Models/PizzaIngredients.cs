using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class PizzaIngredients
{
    [Key]
    public  int PizzaId { get; set; }
    public Pizzas Pizza { get; set; } = null!;
    [Key]
    public int IngredientId { get; set; }
    public Ingredients Ingredient { get; set; } = null!;
}