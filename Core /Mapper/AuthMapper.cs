using Core.Dto;
using Core.Models;

namespace Core.Mapper;

public class AuthMapper
{
    public static Users MapRegDtoToModel(RegisterUserDto model)
    {
        return new Users()
        {
            Email = model.Email!,
            Password = model.Password!,
            LastName = model.LastName,
            FirstName = model.FirstName,
            PhoneNumber = model.PhoneNumber
        };
    }
}