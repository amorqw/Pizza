namespace Core.Dto.RepairOrder;

public class UpdateRepairOrderDto
{
    public string Status { get; set; } = string.Empty;
    public string? ProblemDescription { get; set; }
} 