using Blog.Core.Domain;
using Blog.Infrastructure.EventHandlers;
using Blog.Infrastructure.Events;
using Blog.Infrastructure.Exceptions;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Services;
using Blog.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.UnitTests.Services
{
    public class PostServiceTests
    {
        private Mock<Post> _existingPost;
        private IPostService _postService;
        private Mock<IPostRepository> _postRepository;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            var categoryMock = new Mock<Category>("name");
            categoryMock.SetupGet(x => x.Id).Returns(1);

            _existingPost = new Mock<Post>("title", "content", categoryMock.Object);
            _existingPost.SetupGet(x => x.Id).Returns(1);

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(x => x.GetAsync(1)).ReturnsAsync(_existingPost.Object);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(x => x.GetAsync(1)).ReturnsAsync(categoryMock.Object);

            _eventPublisher = new Mock<IEventPublisher>();

            _postService = new PostService(_postRepository.Object, MockProvider.AutoMapper(), _eventPublisher.Object, categoryRepository.Object);
        }

        [Test]
        public async Task BrowseAsync_ShouldInvokePostRepositoryBrowseAsync()
        {
            await _postService.BrowseAsync();

            _postRepository.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public async Task GetAsync_ShouldInvokePostRepositoryGetAsync()
        {
            var id = MockProvider.RandomInt;

            await _postService.GetAsync(id);

            _postRepository.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public void CreateAsync_ValidData_ShouldSuccess()
        {
            _postService.Invoking(async x => await x.CreateAsync(MockProvider.RandomString, MockProvider.RandomString, 1, MockProvider.RandomString))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityCreatedEvent<Post>>()), Times.Once);
        }

        [Test]
        public void CreateAsync_NotExistingCategory_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.CreateAsync(MockProvider.RandomString, MockProvider.RandomString, 100, MockProvider.RandomString))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Never);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityCreatedEvent<Post>>()), Times.Never);
        }

        [Test]
        public void DeleteAsync_ExistingPost_ShouldSuccess()
        {
            _postService.Invoking(async x => await x.DeleteAsync(_existingPost.Object.Id))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityDeletedEvent<Post>>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingPost_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.DeleteAsync(-1))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.PostNotFound);

            _postRepository.Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Never);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityDeletedEvent<Post>>()), Times.Never);
        }
    }
}
