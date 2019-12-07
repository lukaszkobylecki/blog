using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.EventEntry;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class EventEntryController : ApiControllerBase
    {
        public EventEntryController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) 
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetEventEntries()
        {
            var eventEntries = await FetchAsync(new GetEventEntries());

            return Ok(eventEntries);
        }
    }
}
