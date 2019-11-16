using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public interface IPostRepository : IRepository
    {
        Task<IEnumerable<Post>> BrowseAsync();
        Task<Post> GetAsync(int id);
        Task CreateAsync(Post post);
        Task DeleteAsync(Post post);
    }
}
