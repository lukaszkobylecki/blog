using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task<IEnumerable<User>> BrowseAsync();
        Task DeleteAsync(User user);
    }
}
