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
        private readonly IEventPublisher _eventPublisher;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,
            IEventPublisher eventPublisher)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task<CategoryDto> GetAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            return _mapper.Map<CategoryDto>(category);
        }
        public async Task CreateAsync(string name, string cacheKey)
        {
            var category = new Category(name);
            await _categoryRepository.CreateAsync(category);

            var dto = _mapper.Map<CategoryDto>(category);
            await _eventPublisher.EntityCreated(category, dto, cacheKey);
        }

        public async Task<IEnumerable<CategoryDto>> BrowseAsync()
        {
            var categories = await _categoryRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetOrFailAsync(id);

            await _categoryRepository.DeleteAsync(category);
            await _eventPublisher.EntityDeleted(category);
        }
    }
}
