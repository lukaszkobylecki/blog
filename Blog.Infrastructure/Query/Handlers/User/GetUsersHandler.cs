using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.User;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.User
{
    public class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUsersHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
        {
            return await _userService.BrowseAsync();
        }
    }
}
