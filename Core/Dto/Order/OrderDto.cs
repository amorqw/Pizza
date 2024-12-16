using System.ComponentModel.DataAnnotations;

namespace Core.Dto.Order;

public class OrderDto
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public int StaffId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Status { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}