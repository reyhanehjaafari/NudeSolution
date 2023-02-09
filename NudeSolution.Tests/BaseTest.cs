using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NudeSolution.DataAccess;
using NudeSolution.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NudeSolution.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public MockRepository _mockRepository;
        public DbContextOptions<NudeContext> dbContextOptions;
        public NudeContext _context;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            dbContextOptions = new DbContextOptionsBuilder<NudeContext>().UseInMemoryDatabase("Default").Options;
            _context = new NudeContext(dbContextOptions);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _mockRepository.VerifyAll();
        }
    }
}
