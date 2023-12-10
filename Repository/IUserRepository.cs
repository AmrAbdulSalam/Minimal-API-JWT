using MinimalAPI_JWT.Models;

namespace MinimalAPI_JWT.Repository
{
    public interface IUserRepository
    {
        List<UserDto> GetAllUsers();
    }
}