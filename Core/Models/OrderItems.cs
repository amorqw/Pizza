using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class OrderItems
{
    public int OrderId { get; set; }
    public int PizzaId { get; set; }
    public int Amount { get; set; }
}