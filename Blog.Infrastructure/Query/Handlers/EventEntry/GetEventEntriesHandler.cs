using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries;
using Blog.Infrastructure.Query.Queries.EventEntry;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.EventEntry
{
    public class GetEventEntriesHandler : IQueryHandler<GetEventEntries, IEnumerable<EventEntryDto>>
    {
        private readonly IEventEntryService _eventEntryService;

        public GetEventEntriesHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task<IEnumerable<EventEntryDto>> HandleAsync(GetEventEntries query)
        {
            return await _eventEntryService.BrowseAsync();
        }
    }
}
