using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public AuthService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");

            var hash = _encrypter.GetHash(password, user.Salt);
            if (hash != user.Password)
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
        }
    }
}
