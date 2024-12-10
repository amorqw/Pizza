namespace Core.Dto;

public class StaffDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string Position { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }= DateTime.Now;
}