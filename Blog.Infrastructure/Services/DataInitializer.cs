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

        public DataInitializer(IUserService userService, ICategoryService categoryService)
        {
            _userService = userService;
            _categoryService = categoryService;
        }

        public async Task SeedDataAsync()
        {
            var users = await _userService.BrowseAsync();
            if (users.Any())
                return;

            for (int i = 0; i < 10; i++)
            {
                await _userService.CreateAsync($"user{i}@email.com", "password", $"user{i}", Guid.NewGuid().ToString());
                await _categoryService.CreateAsync($"category{i}", Guid.NewGuid().ToString());
            }
        }
    }
}
