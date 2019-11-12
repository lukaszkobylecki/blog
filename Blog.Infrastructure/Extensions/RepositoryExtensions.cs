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
        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, int id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
                throw new ServiceException(ErrorCodes.UserNotFound, $"User with id: '{id}' was not found.");

            return user;
        }
    }
}
