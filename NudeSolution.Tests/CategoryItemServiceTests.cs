using FluentAssert;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NudeSolution.DataAccess;
using NudeSolution.Entities;
using NudeSolution.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NudeSolution.Tests
{
    [TestFixture]
    public class CategoryItemServiceTests:BaseTest
    {
        private ICategoryItemService categoryItemService;
        private Mock<ILogger<CategoryItemService>> logger;


        [SetUp]
        public void SetUp()
        {
            logger = _mockRepository.Create<ILogger<CategoryItemService>>();
            categoryItemService = new CategoryItemService(_context, logger.Object);
        }

        private void SetLogger(string message) {
            logger.Setup(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()));
        }


        [Test]
        public void AddCategoryItem()
        {
            SetLogger("Category Item has been added successfully!");

            categoryItemService.Add(new CategoryItemEntity { Name = "CategoryItem1", Value = 1 });
          
            var result = categoryItemService.GetAll();
            
            result.Count().ShouldBeEqualTo(1);
            result[0].Name.ShouldBeEqualTo("CategoryItem1");
        }
       
        [Test]
        public void DeleteCategoryItem()
        {
            SetLogger("Category Item has been added successfully!");
            categoryItemService.Add(new CategoryItemEntity { Name = "CategoryItem1", Value = 1 });
            var categoryItem = categoryItemService.GetAll();

            SetLogger("Category Item has been deleted successfully!");
            categoryItemService.Delete(categoryItem[0].CategoryItemId);

            var result = categoryItemService.GetAll();
            result.Count().ShouldBeEqualTo(0);

        }

    }
}