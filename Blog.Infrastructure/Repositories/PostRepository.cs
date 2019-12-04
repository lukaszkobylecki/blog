using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domain;
using Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> BrowseAsync()
            => await _dbContext.Posts
                .Include(x => x.Category)
                .ToListAsync();

        public async Task<Post> GetAsync(Guid id)
            => await _dbContext.Posts
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task CreateAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }
    }
}
