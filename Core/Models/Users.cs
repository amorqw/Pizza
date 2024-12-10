using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Users
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = "net";
}