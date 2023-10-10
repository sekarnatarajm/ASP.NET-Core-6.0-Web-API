using Agree.API.Model;
using Agree.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Agree.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Authentication([FromBody] Authenticatoin authenticatoin)
        {
            try
            {
                var response = _userService.Authentication(authenticatoin);
                return Ok(new
                {
                    StatusCode = HttpStatusCode.OK,
                    Token = response,
                    Username = authenticatoin.Username,
                    Email = "admin@gmail.com"
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
