using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Category
{
    public class UpdateCategoryHandler : ICommandHandler<UpdateCategory>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventPublisher _eventPublisher;

        public UpdateCategoryHandler(ICategoryService categoryService, IEventPublisher eventPublisher)
        {
            _categoryService = categoryService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(UpdateCategory command)
        {
            await _categoryService.UpdateAsync(command.ResourceId, command.Name.TrimOrEmpty());

            var category = await _categoryService.GetOrFailAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new CategoryUpdated(category));
        }
    }
}
