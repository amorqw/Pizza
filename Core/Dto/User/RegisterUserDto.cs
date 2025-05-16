namespace Core.Dto.User;

public class RegisterUserDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public Guid IdRole { get; set; }
}