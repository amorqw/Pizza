using Core.Models;

namespace Core.Interfaces;

public interface IStaff
{
    Task<Staff?> GetStaffById(int id); 
    Task<IEnumerable<Staff>> GetAllStaff(); 
    Task<bool> AddStaff(Staff staff); 
    Task<bool> UpdateStaff(Staff staff); 
    Task<bool> DeleteStaff(int id); 
}