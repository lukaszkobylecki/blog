using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Query.Queries.Post;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Query.Handlers.Post
{
    public class GetPostsHandler : IQueryHandler<GetPosts, IEnumerable<PostDto>>
    {
        private readonly IPostService _postService;

        public GetPostsHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IEnumerable<PostDto>> HandleAsync(GetPosts query)
        {
            return await _postService.BrowseAsync();
        }
    }
}
