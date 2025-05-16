using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Service
{
    [Key]
    public Guid IdService { get; set; }
    public string? ServiceName { get; set; }
    public int Cost { get; set; }
} 