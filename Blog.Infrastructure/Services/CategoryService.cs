using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> BrowseAsync()
        {
            var categories = await _categoryRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> GetOrFailAsync(Guid id)
        {
            var category = await _categoryRepository.GetOrFailAsync(id);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task CreateAsync(Guid id, string name)
        {
            var category = new Category(id, name);
            await _categoryRepository.CreateAsync(category);
        }

        public async Task UpdateAsync(Guid id, string name)
        {
            var category = await _categoryRepository.GetOrFailAsync(id);
            category.SetName(name);

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetOrFailAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
