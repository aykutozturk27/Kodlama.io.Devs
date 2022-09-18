using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPut]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var accessToken = await Mediator.Send(registerCommand);
            return Created("", accessToken);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var accessToken = await Mediator.Send(loginCommand);
            return Ok(accessToken);
        }
    }
}
