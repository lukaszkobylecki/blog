using AutoMapper;
using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class EventEntryService : IEventEntryService
    {
        private readonly IEventEntryRepository _eventEntryRepository;
        private readonly IMapper _mapper;

        public EventEntryService(IEventEntryRepository eventEntryRepository, IMapper mapper)
        {
            _eventEntryRepository = eventEntryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventEntryDto>> BrowseAsync()
        {
            var eventEntries = await _eventEntryRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<EventEntryDto>>(eventEntries);
        }

        public async Task CreateAsync(Guid id, Guid entityId, string entity, string operation, string description = "")
        {
            var eventEntry = new EventEntry(id, entity, entityId, operation, description);
            await _eventEntryRepository.CreateAsync(eventEntry);
        }
    }
}
