using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface ICategoryService : IService
    {
        Task<CategoryDto> GetAsync(int id);
        Task CreateAsync(string name, string cacheKey);
        Task<IEnumerable<CategoryDto>> BrowseAsync();
        Task DeleteAsync(int id);
    }
}
