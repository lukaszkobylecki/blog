using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.User
{
    public class GetUserHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly IUserService _userService;

        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto> HandleAsync(GetUser query)
        {
            return await _userService.GetAsync(query.Id);
        }
    }
}
