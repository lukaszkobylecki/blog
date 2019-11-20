using Blog.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public DataInitializer(IUserService userService, ICategoryService categoryService,
            IPostService postService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _postService = postService;
        }

        public async Task SeedDataAsync()
        {
            var users = await _userService.BrowseAsync();
            if (users.Any())
                return;

            for (int i = 1; i <= 10; i++)
            {
                await _userService.CreateAsync(GuidHelper.GetGuidFromInt(i), $"user{i}@email.com", "password", $"user{i}");
                await _categoryService.CreateAsync(GuidHelper.GetGuidFromInt(i), $"category{i}");
            }

            for (int i = 1; i <= 10; i++)
            {
                await _postService.CreateAsync(GuidHelper.GetGuidFromInt(i), $"title{i}", $"content{i}", GuidHelper.GetGuidFromInt((i % 5) + 1));
            }
        }
    }
}
