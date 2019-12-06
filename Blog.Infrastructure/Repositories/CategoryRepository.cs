using Blog.Core.Domain;
using Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BlogDbContext _dbContext;

        public CategoryRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await _dbContext.Categories.ToListAsync();

        public async Task<Category> GetAsync(Guid id)
            => await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
