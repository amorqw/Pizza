using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Users
{
    [Key]
    public Guid IdUser { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid IdRole { get; set; }
}