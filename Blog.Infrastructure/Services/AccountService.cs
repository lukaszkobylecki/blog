using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEncrypter _encrypter;
        private readonly IUserRepository _userRepository;

        public AccountService(IEncrypter encrypter, IUserRepository userRepository)
        {
            _encrypter = encrypter;
            _userRepository = userRepository;
        }

        public async Task ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            
            var oldPasswordHash = _encrypter.GetHash(currentPassword, user.Salt);
            if (user.Password != oldPasswordHash)
                throw new ServiceException(ErrorCodes.InvalidCredentials, $"Current password is not correct.");

            var salt = _encrypter.GetSalt(newPassword);
            var hash = _encrypter.GetHash(newPassword, salt);
            user.SetPassword(hash, salt);

            await _userRepository.UpdateAsync(user);
        }
    }
}
