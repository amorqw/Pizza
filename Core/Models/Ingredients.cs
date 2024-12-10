using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Ingredients
{
    [Key]
    public int IngredientId { get; set; }
    public string NameIngredient { get; set; }=string.Empty;
}