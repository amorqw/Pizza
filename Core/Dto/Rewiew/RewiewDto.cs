using Core.Models;

namespace Core.Dto.Rewiew;

public class RewiewDto
{
    public int PizzaId { get; set; }
    public Pizzas Pizza { get; set; } = null!;
    public int UserId { get; set; }
    public Users User { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; }= DateTime.Now;
}