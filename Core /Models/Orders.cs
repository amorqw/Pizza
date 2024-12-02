using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Orders
{
    [Key]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public Users User { set; get; } = null!;
    public DateTime OrderData { get; set; } = DateTime.Now;
    public int TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DeliveryAdress { get; set; } = string.Empty;
}