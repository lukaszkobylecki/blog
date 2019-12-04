using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.CommandHandlers;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class EventEntryController : ApiControllerBase
    {
        private readonly IEventEntryService _eventEntryService;

        public EventEntryController(ICommandDispatcher commandDispatcher, IEventEntryService eventEntryService) 
            : base(commandDispatcher)
        {
            _eventEntryService = eventEntryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventEntries()
        {
            var eventEntries = await _eventEntryService.BrowseAsync();

            return Ok(eventEntries);
        }
    }
}
