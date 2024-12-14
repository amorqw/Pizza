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
            Email = model.Email!,
            Password = model.Password!,
            SurName = model.SurName,
            Name = model.Name,
            Phone = model.Phone
        };
    }
}