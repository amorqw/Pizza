using System.Text.Json.Serialization;
using Core.Models;

namespace Core.Dto.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int OrderId { get; set; }
    public int PizzaId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; }= DateTime.Now;
}