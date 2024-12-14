namespace Core.Dto.User;

public class RegisterUserDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}