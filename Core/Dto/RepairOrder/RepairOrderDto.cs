namespace Core.Dto.RepairOrder;

public class RepairOrderDto
{
    public DateTime DateTime { get; set; }
    public Guid ServiceId { get; set; }
    public string CarModel { get; set; } = string.Empty;
    public string CarRegistrationNumber { get; set; } = string.Empty;
    public string? ProblemDescription { get; set; }
    public string Status { get; set; } = "Pending";
} 