using System.Text.Json.Serialization;
using Core.Models;

namespace Core.Dto.Review;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int OrderId { get; set; }
    [JsonIgnore]
    public Orders Order { get; set; } = null!;
    public int PizzaId { get; set; }
    [JsonIgnore]
    public Pizzas Pizza { get; set; } = null!;
    public int UserId { get; set; }
    [JsonIgnore]
    public Users User { get; set; } = null!;
    public int Rating { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime ReviewDate { get; set; }= DateTime.Now;
}