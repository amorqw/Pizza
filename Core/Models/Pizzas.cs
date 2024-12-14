using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Pizzas
{
    [Key]
    public int PizzaId { get; set; }
    public string Title { get; set; }= string.Empty;
    public string Description { get; set; }=string.Empty;
    public int Price { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Receipt { get; set; }
}