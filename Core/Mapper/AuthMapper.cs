using Core.Dto;
using Core.Dto.User;
using Core.Models;

namespace Core.Mapper;

public class AuthMapper
{
    public static Users MapRegDtoToModel(RegisterUserDto model)
    {
        return new Users()
        {
            IdUser = Guid.NewGuid(),
            Email = model.Email!,
            Password = model.Password!,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            Phone = model.Phone,
            IdRole = model.IdRole
        };
    }
}