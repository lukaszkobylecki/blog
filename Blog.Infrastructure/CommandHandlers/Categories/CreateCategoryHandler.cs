﻿using Blog.Common.Extensions;
using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.CommandHandlers.Categories
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(CreateCategory command)
        {
            await _categoryService.CreateAsync(command.Name.TrimOrEmpty(), command.CacheKey);
        }
    }
}