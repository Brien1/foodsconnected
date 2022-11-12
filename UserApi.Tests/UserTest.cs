using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using foods_connected_brien.Controllers;
using TodoApi.Models;
using TodoApi.Models;
using Microsoft.AspNetCore.Builder;
namespace UserApiTest.UnitTests.User_Test
{
    [TestFixture]
    public class UserTest
    {
        private User testuser;
        [SetUp]
        public void SetUp()
        {
            testuser = new User();
        }

        [Test]
        public void test_user_created()
        {

            Assert.IsTrue(testuser != null);

        }
        [Test]
        public void test_user_set_username()
        {

            testuser.username = "brien";
            Assert.IsTrue(testuser.username.Equals("brien"));

        }
        [Test]
        public void test_user_set_userId()
        {

            testuser.userId = 1;
            Assert.IsTrue(testuser.userId == 1);
        }

    }
}