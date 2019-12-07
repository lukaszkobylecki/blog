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
            => await FetchCollection(new GetEventEntries());
    }
}
