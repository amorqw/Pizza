using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class OrderItems
{
    [Key]
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public Orders Order { get; set; } = null!;
    public int PizzaId { get; set; }
    public Pizzas Pizza { get; set; } = null!;
    public int Quantity { get; set; }
    public int ItemPrice { get; set; }
}