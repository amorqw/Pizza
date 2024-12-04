using Core.Dto;
using Core.Models;

namespace Core.Interfaces;

public interface IStaff
{
    Task<Staff?> GetStaffById(int id); 
    Task<IEnumerable<Staff>> GetAllStaff(); 
    Task<bool> AddStaff(StaffDto staff); 
    Task<bool> UpdateStaff(UpdateStaffDto staff); 
    Task<bool> DeleteStaff(int id); 
}