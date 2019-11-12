using AutoMapper;
using Moq;
using Blog.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.UnitTests.Mocks
{
    public class MockProvider
    {
        public static Mock<IMapper> AutoMapper() 
            => new Mock<IMapper>();

        public static Mock<IEncrypter> Encrypter()
        {
            var encrypterMock = new Mock<IEncrypter>();

            encrypterMock.Setup(x => x.GetSalt(It.IsAny<string>())).Returns((string salt) => salt);
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns((string password, string salt) => password);

            return encrypterMock;
        }
    }
}
