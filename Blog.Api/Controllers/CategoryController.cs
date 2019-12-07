using System;
using System.Threading.Tasks;
using Blog.Infrastructure.Command.Handlers;
using Blog.Infrastructure.Command.Commands.Category;
using Microsoft.AspNetCore.Mvc;
using Blog.Infrastructure.Query.Handlers;
using Blog.Infrastructure.Query.Queries.Category;
using Blog.Api.Extensions;

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
            => await FetchCollection(new GetCategories());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
            => await FetchSingle(new GetCategory().Bind(x => x.Id, id));

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategory command)
            => await CreateAsync(command.Bind(x => x.ResourceId, Guid.NewGuid()), "category");

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategory command)
            => await ExecuteAsync(command.Bind(x => x.ResourceId, id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
            => await ExecuteAsync(new DeleteCategory().Bind(x => x.ResourceId, id));
    }
}
