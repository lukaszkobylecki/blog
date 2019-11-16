using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task CreateAsync(string email, string password, string username, string cacheKey);
        Task<UserDto> GetAsync(string email);
        Task<UserDto> GetAsync(int id);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task DeleteAsync(int id);
    }
}
