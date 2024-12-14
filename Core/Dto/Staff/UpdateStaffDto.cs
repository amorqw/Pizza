namespace Core.Dto;

public class UpdateStaffDto
{
    public int StaffId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; }= string.Empty;
}