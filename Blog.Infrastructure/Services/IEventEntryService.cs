using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public interface IEventEntryService : IService
    {
        Task<IEnumerable<EventEntryDto>> BrowseAsync();
        Task CreateAsync(Guid id, Guid entityId, string entity, string operation, string description = "");
    }
}
