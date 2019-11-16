using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.CommandHandlers;
using Blog.Infrastructure.Commands.Category;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Api.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemoryCache _cache;

        public CategoryController(ICommandDispatcher commandDispatcher, ICategoryService categoryService,
            IMemoryCache cache) 
            : base(commandDispatcher)
        {
            _categoryService = categoryService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.BrowseAsync();

            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await _categoryService.GetAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory command)
        {
            await DispatchAsync(command);

            var category = _cache.Get<CategoryDto>(command.CacheKey);

            return Created($"category/{category.Id}", category);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategory { Id = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
