namespace Core.Models;

public class Pizzeria
{
    public int PizzeriaId { get; set; }
    public string Title { get; set; }=string.Empty;
    public int Rating { get; set; } 
    public string Address { get; set; } = string.Empty;
    public int CourierAmount { get; set; } 
}