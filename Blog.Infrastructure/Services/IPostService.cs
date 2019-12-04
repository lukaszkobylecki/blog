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
        Task<PostDto> GetAsync(Guid id);
        Task<PostDto> GetOrFailAsync(Guid id);
        Task CreateAsync(Guid id, string title, string content, Guid categoryId);
        Task DeleteAsync(Guid id);
    }
}
