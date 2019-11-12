using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Commands.Users;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(ICommandDispatcher commandDispatcher, IUserService userService) 
            : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.BrowseAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser command)
        {
            await DispatchAsync(command);

            var user = await _userService.GetAsync(command.Email);

            return Created($"users/{user.Id}", user);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            await DispatchAsync(new DeleteUser());

            return NoContent();
        }
    }
}
