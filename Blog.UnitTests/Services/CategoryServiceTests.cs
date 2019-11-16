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
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<Category> _existingCategory;
        private ICategoryService _categoryService;
        private Mock<ICategoryRepository> _categoryRepository;
        private Mock<IEventPublisher> _eventPublisher;

        [SetUp]
        public void SetUp()
        {
            _existingCategory = new Mock<Category>("category");
            _existingCategory.SetupGet(x => x.Id).Returns(1);

            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryRepository.Setup(x => x.GetAsync(1)).ReturnsAsync(_existingCategory.Object);

            _eventPublisher = new Mock<IEventPublisher>();

            _categoryService = new CategoryService(_categoryRepository.Object, MockProvider.AutoMapper(), _eventPublisher.Object);
        }

        [Test]
        public async Task BrowseAsync_ShouldInvokeCategoryRepositoryBrowseAsync()
        {
            await _categoryService.BrowseAsync();

            _categoryRepository.Verify(x => x.BrowseAsync(), Times.Once);
        }

        [Test]
        public async Task GetAsync_ShouldInvokeCategoryRepositoryGetAsync()
        {
            var id = MockProvider.RandomInt;

            await _categoryService.GetAsync(id);

            _categoryRepository.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public void CreateAsync_ShouldSuccess()
        {
            _categoryService.Invoking(async x => await x.CreateAsync(MockProvider.RandomString, MockProvider.RandomString))
                .Should()
                .NotThrow();

            _categoryRepository.Verify(x => x.CreateAsync(It.IsAny<Category>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityCreatedEvent<Category>>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_ExistingCategory_ShouldSuccess()
        {
            _categoryService.Invoking(async x => await x.DeleteAsync(_existingCategory.Object.Id))
                .Should()
                .NotThrow();

            _categoryRepository.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Once);
            _eventPublisher.Verify(x => x.PublishAsync(It.IsAny<EntityDeletedEvent<Category>>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingCategory_ShouldThrowError()
        {
            _categoryService.Invoking(async x => await x.DeleteAsync(-1))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.CategoryNotFound);

            _categoryRepository.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Never);
        }
    }
}
