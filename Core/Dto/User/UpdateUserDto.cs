using System.ComponentModel.DataAnnotations;

namespace Core.Dto;

public class UpdateUserDto
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }=string.Empty;
    public string SurName { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}