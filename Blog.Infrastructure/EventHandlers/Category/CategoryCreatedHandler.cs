using Blog.Infrastructure.Events.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EventHandlers.Category
{
    public class CategoryCreatedHandler : IEventHandler<CategoryCreated>
    {
        private readonly IEventEntryService _eventEntryService;

        public CategoryCreatedHandler(IEventEntryService eventEntryService)
        {
            _eventEntryService = eventEntryService;
        }

        public async Task HandleAsync(CategoryCreated @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), $"Event '{typeof(CategoryCreated)}' can not be null.");

            await _eventEntryService.CreateAsync(Guid.NewGuid(), @event.Category.Id,
                "category", "create", $"Name: '{@event.Category.Name}'");
        }
    }
}
