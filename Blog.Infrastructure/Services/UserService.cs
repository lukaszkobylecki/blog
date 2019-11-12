using AutoMapper;
using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Extensions;
using Blog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encrypter = encrypter;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            return _mapper.Map<UserDto>(user);

        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(string email, string password, string username)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email: '{email}' already exists.");

            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);

            user = new User(email, hash, salt, username);
            await _userRepository.AddAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetOrFailAsync(id);
            
            await _userRepository.DeleteAsync(user);
        }
    }
}
