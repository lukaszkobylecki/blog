using AutoMapper;
using Moq;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.Infrastructure.EventHandlers;
using System.Linq;

namespace Blog.UnitTests.Mocks
{
    public class MockProvider
    {
        private static readonly Random _random = new Random();
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static int RandomInt => _random.Next(100);
        public static string RandomString => new string(Enumerable.Repeat(_chars, 10).Select(x => x[_random.Next(x.Length)]).ToArray());

        public static IMapper AutoMapper(Action<Mock<IMapper>> setup = null)
        {
            var autoMapperMock = new Mock<IMapper>();

            setup?.Invoke(autoMapperMock);

            return autoMapperMock.Object;
        }

        public static IEncrypter Encrypter(Action<Mock<IEncrypter>> setup = null)
        {
            var encrypterMock = new Mock<IEncrypter>();

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns((string salt) => salt);
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns((string password, string salt) => password);

            setup?.Invoke(encrypterMock);

            return encrypterMock.Object;
        }
    }
}
