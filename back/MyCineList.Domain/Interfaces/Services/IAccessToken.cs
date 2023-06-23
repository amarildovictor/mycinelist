using MyCineList.Domain.Entities.Auth;

namespace MyCineList.Domain.Interfaces.Services
{
    public interface IAccessToken
    {
        string GenerateAccessToken(User user);
    }
}