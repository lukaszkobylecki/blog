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

namespace Blog.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICategoryRepository _categoryRepository;

        public PostService(IPostRepository postRepository, IMapper mapper,
            IEventPublisher eventPublisher, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<PostDto>> BrowseAsync()
        {
            var posts = await _postRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetAsync(int id)
        {
            var post = await _postRepository.GetAsync(id);

            return _mapper.Map<PostDto>(post);
        }

        public async Task CreateAsync(string title, string content, int categoryId, string cacheKey)
        {
            var category = await _categoryRepository.GetOrFailAsync(categoryId);

            var post = new Post(title, content, category);
            await _postRepository.CreateAsync(post);

            var dto = _mapper.Map<PostDto>(post);
            await _eventPublisher.EntityCreated(post, dto, cacheKey);
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _postRepository.GetOrFailAsync(id);

            await _postRepository.DeleteAsync(post);
            await _eventPublisher.EntityDeleted(post);
        }
    }
}
