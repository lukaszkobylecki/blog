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
    public class EventEntryRepository : IEventEntryRepository
    {
        private readonly BlogDbContext _dbContext;

        public EventEntryRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EventEntry>> BrowseAsync()
            => await _dbContext.EventEntries.ToListAsync();

        public async Task CreateAsync(EventEntry eventEntry)
        {
            await _dbContext.EventEntries.AddAsync(eventEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
