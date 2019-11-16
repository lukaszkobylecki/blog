using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Commands.Auth;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Common.Extensions;

namespace Blog.Infrastructure.CommandHandlers.Auth
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandler(IAuthService authService, IUserService userService, 
            IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _authService = authService;
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _authService.LoginAsync(command.Email.TrimToLower(), command.Password.TrimOrEmpty());

            var user = await _userService.GetAsync(command.Email.TrimToLower());
            var token = _jwtHandler.CreateToken(user.Id);
            _cache.SetShort(command.CacheKey, token);
        }
    }
}
