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
    public class CategoryItemServiceTests
    {
        private ICategoryItemService categoryItemService;
        private Mock<ILogger<CategoryItemService>> logger;
        private MockRepository _mockRepository;

        private DbContextOptions<NudeContext> dbContextOptions;
        private NudeContext _context;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            dbContextOptions = new DbContextOptionsBuilder<NudeContext>().UseInMemoryDatabase("Default").Options;
            _context = new NudeContext(dbContextOptions);

            logger = _mockRepository.Create<ILogger<CategoryItemService>>();

            categoryItemService = new CategoryItemService(_context, logger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _mockRepository.VerifyAll();
        }

        [Test]
        public void AddCategoryItem()
        {     
            logger.Setup(
                 x => x.Log(
                     LogLevel.Information,
                     It.IsAny<EventId>(),
                     It.Is<It.IsAnyType>((o, t) => string.Equals("Category Item has been added successfully!", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                     It.IsAny<Exception>(),
                     It.IsAny<Func<It.IsAnyType, Exception, string>>()));

            categoryItemService.Add(new CategoryItemEntity { Name = "name", Value = 1 });
          
            var result = categoryItemService.GetAll();
            
            result.Count().ShouldBeEqualTo(1);

        }

    }
}