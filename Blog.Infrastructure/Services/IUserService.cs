using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task<UserDto> GetAsync(Guid id);
        Task<UserDto> GetAsync(string email);
        Task<UserDto> GetOrFailAsync(Guid id);
        Task<UserDto> GetOrFailAsync(string email);
        Task CreateAsync(Guid id, string email, string password, string username);
        Task DeleteAsync(Guid id);

    }
}
