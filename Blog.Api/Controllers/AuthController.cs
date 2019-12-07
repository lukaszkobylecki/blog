using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Command.Commands.Auth;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Handlers;

namespace Blog.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;

        public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
            IMemoryCache cache) 
            : base(commandDispatcher, queryDispatcher)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login command)
        {
            await DispatchAsync(command);
            
            var token = _cache.Get<JwtDto>(command.Request.Id.ToString());

            return Ok(token);
        }
    }
}
