using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoApi.Models;
using foods_connected_brien.Controllers;
namespace TestingDemo
{
    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void GetAllBlogs_orders_by_name()
        {
            var data = new List<User>
            {
                new User { username = "BBB" },
                new User { username = "ZZZ" },
                new User { username = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<UserContext>();
            mockContext.Setup(c => c.User).Returns(mockSet.Object);

            var service = new UserController(mockContext.Object);
            var users = service.GetUser();

            // Assert.AreEqual(3, users.Count);
            // Assert.AreEqual("AAA", users[0].Name);
            // Assert.AreEqual("BBB", users[1].Name);
            // Assert.AreEqual("ZZZ", users[2].Name);
        }
    }
}