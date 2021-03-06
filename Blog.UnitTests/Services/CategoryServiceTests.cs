﻿using Blog.Common.Helpers;
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
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<Category> _existingCategory;
        private ICategoryService _categoryService;
        private Mock<ICategoryRepository> _categoryRepository;

        [SetUp]
        public void SetUp()
        {
            _existingCategory = new Mock<Category>(GuidHelper.GetGuidFromInt(1), "category");

            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryRepository.Setup(x => x.GetAsync(_existingCategory.Object.Id)).ReturnsAsync(_existingCategory.Object);

            _categoryService = new CategoryService(_categoryRepository.Object, MockProvider.AutoMapper());
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
            var id = GuidHelper.GetGuidFromInt(MockProvider.RandomInt);

            await _categoryService.GetAsync(id);

            _categoryRepository.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Test]
        public void CreateAsync_ShouldSuccess()
        {
            _categoryService.Invoking(async x => await x.CreateAsync(Guid.NewGuid(), MockProvider.RandomString))
                .Should()
                .NotThrow();

            _categoryRepository.Verify(x => x.CreateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_ExistingCategory_ShouldSucces()
        {
            _categoryService.Invoking(async x => await x.UpdateAsync(_existingCategory.Object.Id, "new-name"))
                .Should()
                .NotThrow();

            _categoryRepository.Verify(x => x.UpdateAsync(It.IsAny<Category>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_NotExistingCategory_ShouldSucces()
        {
            _categoryService.Invoking(async x => await x.UpdateAsync(GuidHelper.GetGuidFromInt(100), "new-name"))
                .Should()
                .Throw<ServiceException>()
                .Which.Code.Should().Be(ErrorCodes.CategoryNotFound);

            _categoryRepository.Verify(x => x.UpdateAsync(It.IsAny<Category>()), Times.Never);
        }

        [Test]
        public void DeleteAsync_ExistingCategory_ShouldSuccess()
        {
            _categoryService.Invoking(async x => await x.DeleteAsync(_existingCategory.Object.Id))
                .Should()
                .NotThrow();

            _categoryRepository.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_NotExistingCategory_ShouldThrowError()
        {
            _categoryService.Invoking(async x => await x.DeleteAsync(GuidHelper.GetGuidFromInt(100)))
                .Should()
                .Throw<ServiceException>()
                .And.Code.Should().Be(ErrorCodes.CategoryNotFound);

            _categoryRepository.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Never);
        }
    }
}
