using Microsoft.AspNetCore.Mvc;
using MinimalAPI_JWT.Models;
using MinimalAPI_JWT.Services;

namespace MinimalAPI_JWT.Controller
{
    [ApiController]
    [Route("/api/authenticate")]
    public class UserController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserController(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public ActionResult AuthenticateUser(UserDto user)
        {
            var token = _tokenGenerator.GenerateToken(user);
            if (token == null)
            {
                return NotFound("No User found ....");
            }
            return Ok(token);
        }
    }
}