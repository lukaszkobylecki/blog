using Blog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public interface IEventEntryRepository : IRepository
    {
        Task<IEnumerable<EventEntry>> BrowseAsync();
        Task CreateAsync(EventEntry eventEntry);
    }
}
