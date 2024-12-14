using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Users
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }=string.Empty;
    public string SurName { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = "net";
}