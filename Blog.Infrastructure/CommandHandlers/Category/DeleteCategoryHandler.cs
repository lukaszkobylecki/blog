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
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventPublisher _eventPublisher;

        public DeleteCategoryHandler(ICategoryService categoryService, IEventPublisher eventPublisher)
        {
            _categoryService = categoryService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(DeleteCategory command)
        {
            await _categoryService.DeleteAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new CategoryDeleted(command.ResourceId));
        }
    }
}
