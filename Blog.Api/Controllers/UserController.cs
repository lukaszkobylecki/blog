using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Commands.Users;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.CommandHandlers;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.DTO;

namespace Blog.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public UserController(ICommandDispatcher commandDispatcher, IUserService userService,
            IMemoryCache cache)
            : base(commandDispatcher)
        {
            _userService = userService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
        {
            await DispatchAsync(command);

            var user = _cache.Get<UserDto>(command.CacheKey);

            return Created($"user/{user.Id}", user);
        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUser { Id = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
