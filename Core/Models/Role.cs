using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Role
{
    [Key]
    public Guid IdRole { get; set; }
    public string RoleName { get; set; } = string.Empty;
} 