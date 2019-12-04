using Blog.Core.Domain;
using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with id: '{id}' was not found.");

            return user;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, string email)
        {
            var user = await userRepository.GetAsync(email);
            if (user == null)
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with email: '{email}' was not found.");

            return user;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository categoryRepository, Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category == null)
                throw new ServiceException(ErrorCodes.CategoryNotFound, $"Category with id: '{id}' was not found.");

            return category;
        }

        public static async Task<Post> GetOrFailAsync(this IPostRepository postRepository, Guid id)
        {
            var post = await postRepository.GetAsync(id);
            if (post == null)
                throw new ServiceException(ErrorCodes.PostNotFound, $"Post with id: '{id}' was not found.");

            return post;
        }
    }
}
