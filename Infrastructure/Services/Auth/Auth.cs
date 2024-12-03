using Core.Interfaces.Auth;
using Core.Models;

namespace Core.Services.Auth;

public class Auth:IAuth
{
    private readonly IAuth auth;
    public Auth(IAuth auth)
    {
        this.auth = auth;
    }

    public async Task<int> CreateUser(Users user)
    {
        return await auth.CreateUser(user);
    }
}