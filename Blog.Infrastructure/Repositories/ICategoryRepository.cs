using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public interface ICategoryRepository : IRepository
    {
        Task<Category> GetAsync(int id);
        Task CreateAsync(Category category);
        Task<IEnumerable<Category>> BrowseAsync();
        Task DeleteAsync(Category category);
    }
}
