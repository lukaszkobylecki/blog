using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.Category
{
    public class GetCategoryHandler : IQueryHandler<GetCategory, CategoryDto>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryDto> HandleAsync(GetCategory query)
        {
            return await _categoryService.GetAsync(query.Id);
        }
    }
}
