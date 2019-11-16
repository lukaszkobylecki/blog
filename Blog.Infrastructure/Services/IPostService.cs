using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IPostService : IService
    {
        Task<IEnumerable<PostDto>> BrowseAsync();
        Task<PostDto> GetAsync(int id);
        Task CreateAsync(string title, string content, int categoryId, string cacheKey);
        Task DeleteAsync(int id);
    }
}
