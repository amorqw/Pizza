using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class RepairOrder
{
    [Key]
    public Guid IdOrder { get; set; }
    
    [ForeignKey("Service")]
    public Guid IdService { get; set; }
    public Service? Service { get; set; }
    
    [ForeignKey("User")]
    public Guid IdUser { get; set; }
    public Users? User { get; set; }
    
    public string CarRegistrationNumber { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public string? ProblemDescription { get; set; }
    public DateTime? DateTime { get; set; }
    public string? Status { get; set; }
} 