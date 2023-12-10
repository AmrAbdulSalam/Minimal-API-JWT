using MinimalAPI_JWT.Models;

namespace MinimalAPI_JWT.Services
{
    public interface IJwtTokenGenerator
    {
        JwtToken GenerateToken(UserDto user);

        bool ValidateToken(JwtToken token);
    }
}