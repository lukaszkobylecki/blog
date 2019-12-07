using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.Category;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.Category
{
    public class GetCategoriesHandler : IQueryHandler<GetCategories, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryService _categoryService;

        public GetCategoriesHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IEnumerable<CategoryDto>> HandleAsync(GetCategories query)
        {
            return await _categoryService.BrowseAsync();
        }
    }
}
