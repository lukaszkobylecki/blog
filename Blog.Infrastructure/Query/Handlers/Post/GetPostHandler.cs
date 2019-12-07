using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.Post
{
    public class GetPostHandler : IQueryHandler<GetPost, PostDto>
    {
        private readonly IPostService _postService;

        public GetPostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<PostDto> HandleAsync(GetPost query)
        {
            return await _postService.GetAsync(query.Id);
        }
    }
}
