using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events.Category;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Category
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventPublisher _eventPublisher;

        public CreateCategoryHandler(ICategoryService categoryService, IEventPublisher eventPublisher)
        {
            _categoryService = categoryService;
            _eventPublisher = eventPublisher;
        }

        public async Task HandleAsync(CreateCategory command)
        {
            await _categoryService.CreateAsync(command.ResourceId, command.Name.TrimOrEmpty());

            var category = await _categoryService.GetOrFailAsync(command.ResourceId);
            await _eventPublisher.PublishAsync(new CategoryCreated(category));
        }
    }
}
