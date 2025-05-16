using Core.Dto;
using Core.Dto.User;
using Core.Models;

namespace Core.Interfaces;

public interface IUser
{
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users> GetUser(Guid id);
    Task<Users> UpdateUser(UpdateUserDto userDto, Guid id);
    Task<bool> DeleteUser(Guid id);
    Task<int> CreateUser(Users users);
    Task<Users> GetUserByEmail(string email);
}