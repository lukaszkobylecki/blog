using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<IEnumerable<User>> BrowseAsync();
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
