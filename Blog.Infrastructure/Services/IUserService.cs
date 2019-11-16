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
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetAsync(string email);
        Task CreateAsync(string email, string password, string username, string cacheKey);
        Task DeleteAsync(int id);
    }
}
