using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_JWT.Models;
using MinimalAPI_JWT.Repository;
using MinimalAPI_JWT.Services;

namespace MinimalAPI_JWT.Controller
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersInfoController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public UsersInfoController(IUserRepository userRepository , IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers()
        {
            var authrization = Request.Headers["Authorization"];
            string token = authrization[0].Substring(7);
            
            if (!_jwtTokenGenerator.ValidateToken(new JwtToken { Token = token}))
            {
                return Unauthorized();
            }

            return Ok(_userRepository.GetAllUsers());
        }
    }
}