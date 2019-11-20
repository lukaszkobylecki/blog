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
        Task<IEnumerable<CategoryDto>> BrowseAsync();
        Task<CategoryDto> GetAsync(Guid id);
        Task CreateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
    }
}
