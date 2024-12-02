using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Staff
{
    [Key]
    public int StaffId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }= DateTime.Now;
}