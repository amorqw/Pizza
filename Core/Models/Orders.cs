using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Orders
{
    [Key]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public Users User { set; get; } = null!;
    public int StaffId { get; set; }
    public Staff Staff { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.Now;
    public string Status { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}