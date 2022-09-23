using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerCommand)
        {
            var accessToken = await Mediator.Send(registerCommand);
            return Created("", accessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginCommand)
        {
            var accessToken = await Mediator.Send(loginCommand);
            return Ok(accessToken);
        }
    }
}
