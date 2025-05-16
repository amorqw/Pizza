namespace Core.Dto.RepairOrder;

public class CreateRepairOrderDto
{
    public Guid IdService { get; set; }
    public string CarRegistrationNumber { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public string? ProblemDescription { get; set; }
    public DateTime DateTime { get; set; }
} 