using Blog.Infrastructure.Event.Events.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Event.Handlers.Category
{
    public class CategoryDeletedHandler : IEventHandler<CategoryDeleted>
    {
        private readonly IEventEntryService _eventEntryService;

        public CategoryDeletedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(CategoryDeleted @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(CategoryDeleted)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Id, "category", "delete");
        }
    }
}
