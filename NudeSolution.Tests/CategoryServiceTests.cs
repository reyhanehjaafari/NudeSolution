using FluentAssert;
using Microsoft.Extensions.Logging;
using Moq;
using NudeSolution.Entities;
using NudeSolution.Services;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NudeSolution.Tests
{
    [TestFixture]
    public class CategoryServiceTests : BaseTest
    {
        private ICategoryService categoryService;
        private Mock<ILogger<CategoryService>> categoryLogger;
        private ICategoryItemService categoryItemService;
        private Mock<ILogger<CategoryItemService>> categoryItemlogger;

        private void SetLogger<T>(Mock<ILogger<T>> logger, LogLevel logLevel, string message)
        {
            logger.Setup(
                x => x.Log(
                    logLevel,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()));
        }

        [SetUp]
        public void SetUp()
        {
            categoryLogger = _mockRepository.Create<ILogger<CategoryService>>();
            categoryService = new CategoryService(_context, categoryLogger.Object);

            categoryItemlogger = _mockRepository.Create<ILogger<CategoryItemService>>();
            categoryItemService = new CategoryItemService(_context, categoryItemlogger.Object);
        }



        [Test]
        public void AddCategoryItem()
        {

            SetLogger(categoryLogger, LogLevel.Information, "Category has been added successfully!");

            categoryService.Add(new CategoryEntity { Name = "Category1" });

            var result = categoryService.GetCategories();

            result.Count().ShouldBeEqualTo(1);
            result[0].Name.ShouldBeEqualTo("Category1");

        }

        [Test]
        public void GetAll()
        {

            SetLogger(categoryLogger, LogLevel.Information, "Category has been added successfully!");
           

            categoryService.Add(new CategoryEntity { Name = "Category1" });
            categoryService.Add(new CategoryEntity { Name = "Category2" });

            var result = categoryService.GetCategories();
            var category= result[0].CategoryId;

            SetLogger(categoryItemlogger, LogLevel.Information, "Category Item has been added successfully!");

            categoryItemService.Add(new CategoryItemEntity { Name = "CategoryItem1", Value = 1, CategoryId = category });

            categoryItemService.Add(new CategoryItemEntity { Name = "CategoryItem2", Value = 2, CategoryId = category });

            SetLogger(categoryLogger, LogLevel.Information, "Category fetched successfully");
            var categoryWithItems = categoryService.GetAll();
            categoryWithItems.Item1.Count().ShouldBeEqualTo(2);
            categoryWithItems.Item1[0].CategoryItems.Count.ShouldBeEqualTo(2);
            categoryWithItems.Item1[1].CategoryItems.Count.ShouldBeEqualTo(0);
            categoryWithItems.Item2.ShouldBeEqualTo(3);
        }

    }
}
