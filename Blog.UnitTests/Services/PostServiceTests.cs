using Blog.Common.Helpers;
using Blog.Core.Domain;
using Blog.Infrastructure.Event.Handlers;
using Blog.Infrastructure.Event.Events;
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

        [SetUp]
        public void SetUp()
        {
            var categoryMock = new Mock<Category>(GuidHelper.GetGuidFromInt(1), "name");

            _existingPost = new Mock<Post>(GuidHelper.GetGuidFromInt(1), "title", "content", categoryMock.Object);

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(x => x.GetAsync(_existingPost.Object.Id)).ReturnsAsync(_existingPost.Object);
            _postRepository.Setup(x => x.GetAsync(GuidHelper.GetGuidFromInt(100))).ReturnsAsync(() => null);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(x => x.GetAsync(categoryMock.Object.Id)).ReturnsAsync(categoryMock.Object);

            _postService = new PostService(_postRepository.Object, MockProvider.AutoMapper(), categoryRepository.Object);
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
            var id = GuidHelper.GetGuidFromInt(MockProvider.RandomInt);

            await _postService.GetAsync(id);

            _postRepository.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public void CreateAsync_ValidData_ShouldSuccess()
        {
            _postService.Invoking(async x => await x.CreateAsync(Guid.NewGuid(), MockProvider.RandomString, MockProvider.RandomString, GuidHelper.GetGuidFromInt(1)))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void CreateAsync_NotExistingCategory_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.CreateAsync(Guid.NewGuid(), MockProvider.RandomString, MockProvider.RandomString, GuidHelper.GetGuidFromInt(100)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.CategoryNotFound);

            _postRepository.Verify(x => x.CreateAsync(It.IsAny<Post>()), Times.Never);
        }

        [Test]
        public void UpdateAsync_ExistingPost_ShouldSuccess()
        {
            _postService.Invoking(async x => await x.UpdateAsync(_existingPost.Object.Id, MockProvider.RandomString, MockProvider.RandomString, GuidHelper.GetGuidFromInt(1)))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_NotExistingPost_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.UpdateAsync(GuidHelper.GetGuidFromInt(100), MockProvider.RandomString, MockProvider.RandomString, GuidHelper.GetGuidFromInt(1)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.PostNotFound);

            _postRepository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
        }

        [Test]
        public void UpdateAsync_NotExistingCategory_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.CreateAsync(GuidHelper.GetGuidFromInt(1), MockProvider.RandomString, MockProvider.RandomString, GuidHelper.GetGuidFromInt(100)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.CategoryNotFound);

            _postRepository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Never);
        }


        [Test]
        public void DeleteAsync_ExistingPost_ShouldSuccess()
        {
            _postService.Invoking(async x => await x.DeleteAsync(_existingPost.Object.Id))
                .Should()
                .NotThrow();

            _postRepository.Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingPost_ShouldThrowError()
        {
            _postService.Invoking(async x => await x.DeleteAsync(GuidHelper.GetGuidFromInt(100)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.PostNotFound);

            _postRepository.Verify(x => x.DeleteAsync(It.IsAny<Post>()), Times.Never);
        }
    }
}
