using Core.Models;

namespace Core.Dto.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int PizzaId { get; set; }
    public Pizzas Pizza { get; set; } = null!;
    public int UserId { get; set; }
    public Users User { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; }= DateTime.Now;
}