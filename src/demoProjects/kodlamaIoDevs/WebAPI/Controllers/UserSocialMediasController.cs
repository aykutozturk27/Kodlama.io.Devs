using Application.Features.UserSocialMedias.Commands.CreateUserSocialMedia;
using Application.Features.UserSocialMedias.Commands.DeleteUserSocialMedia;
using Application.Features.UserSocialMedias.Commands.UpdateUserSocialMedia;
using Application.Features.UserSocialMedias.Dtos;
using Application.Features.UserSocialMedias.Models;
using Application.Features.UserSocialMedias.Queries.GetListUserSocialMedia;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSocialMediasController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserSocialMediaCommand createUserSocialMediaCommand)
        {
            CreatedUserSocialMediaDto result = await Mediator.Send(createUserSocialMediaCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserSocialMediaCommand updateUserSocialMediaCommand)
        {
            UpdatedUserSocialMediaDto result = await Mediator.Send(updateUserSocialMediaCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserSocialMediaCommand deleteUserSocialMediaCommand)
        {
            DeletedUserSocialMediaDto result = await Mediator.Send(deleteUserSocialMediaCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserSocialMediaQuery getListUserSocialMediaQuery = new() { PageRequest = pageRequest };
            UserSocialMediaListModel result = await Mediator.Send(getListUserSocialMediaQuery);
            return Ok(result);
        }
    }
}
