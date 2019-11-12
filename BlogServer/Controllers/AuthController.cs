using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Commands.Auth;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Services;

namespace Blog.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;

        public AuthController(ICommandDispatcher commandDispatcher, IMemoryCache cache) 
            : base(commandDispatcher)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = _cache.GetJwt(command.TokenId);

            return Ok(token);
        }
    }
}
