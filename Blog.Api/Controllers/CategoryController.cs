using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Command.Commands.Category;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.Category;

namespace Blog.Api.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        public CategoryController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) 
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await FetchAsync(new GetCategories());

            return Ok(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var category = await FetchAsync(new GetCategory { Id = id });
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory command)
        {
            command.ResourceId = Guid.NewGuid();
            await DispatchAsync(command);

            return Created($"category/{command.ResourceId}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategory command)
        {
            command.ResourceId = id;
            await DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategory { ResourceId = id };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
