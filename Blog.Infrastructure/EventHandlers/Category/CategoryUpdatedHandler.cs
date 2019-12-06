using Blog.Infrastructure.Events.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Category
{
    public class CategoryUpdatedHandler : IEventHandler<CategoryUpdated>
    {
        private readonly IEventEntryService _eventEntryService;

        public CategoryUpdatedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(CategoryUpdated @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(CategoryUpdated)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Category.Id,
                "category", "update", $"Name: {@event.Category.Name}");
        }
    }
}
