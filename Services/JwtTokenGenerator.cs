using MinimalAPI_JWT.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI_JWT.Models;

namespace MinimalAPI_JWT.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public JwtTokenGenerator(IConfiguration configuration , IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public JwtToken GenerateToken(UserDto user)
        {
            if (!_userRepository.GetAllUsers().Any(x => x.Username == user.Username && x.Password == user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["Token:Key"]);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name , user.Username));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return new JwtToken { Token = tokenHandler.WriteToken(token)};
        }

        public bool ValidateToken(JwtToken token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Token:Key"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                tokenHandler.ValidateToken(token.Token , validationParameters ,out SecurityToken validatedToken);
                return true;
            }
            catch(SecurityTokenValidationException ex)
            {
                return false;
            }
        }
    }
}