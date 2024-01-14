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
        /// <summary>
        /// Login to a registed user in the Repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Returns the token for user</response>
        /// <response code="404">If the user is not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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