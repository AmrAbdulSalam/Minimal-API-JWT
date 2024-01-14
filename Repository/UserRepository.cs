using MinimalAPI_JWT.Models;

namespace MinimalAPI_JWT.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<UserDto> _users;

        public UserRepository()
        {
            _users = new List<UserDto>
            {
                new UserDto {Username = "amr" , Password = "123"},
                new UserDto {Username = "ahmad" , Password = "543"},
                new UserDto {Username = "samer" , Password = "842"},
                new UserDto {Username = "tommy" , Password = "8362"}
            };
        }

        public List<UserDto> GetAllUsers()
        {
            return _users;
        }
    }
}