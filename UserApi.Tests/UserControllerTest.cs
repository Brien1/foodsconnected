using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using foods_connected_brien.Controllers;
using TodoApi.Models;
using Microsoft.AspNetCore.Builder;
using Moq;
using System.Text.Json;

namespace UserApiTest.UnitTests.User_Controller_Test
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController controller;
        private User test_user;
        [SetUp]
        public void SetUp()
        {

            controller = new UserController();
            test_user = new User();
            test_user.username = "brien";
        }
        [TearDown]
        public void TearDown() { }

        [Test]
        public void test_get_set_user()
        {

            test_user.userId = 1;
            var resp1 = controller.PostUser(test_user);
            var resp2 = controller.GetUser(1);

            Assert.IsTrue(resp2.Result.Value.username.Equals("brien"));


        }

        [Test]
        public async Task test_get_set_user_sameNameError()
        {
            var resp1 = await controller.PostUser(test_user);
            var resp2 = await controller.PostUser(test_user);
            var r = resp2.Result.ToString();
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.BadRequestObjectResult", r);

        }

        [Test]
        public async Task test_alter_username()
        {
            test_user.userId = 2;
            long userid = test_user.userId;

            var resp1 = await controller.PostUser(test_user);
            var resp2 = controller.GetUser(userid);
            Assert.IsTrue(resp2.Result.Value.username.Equals("brien"));
            User f = resp2.Result.Value;
            f.username = "Dave";
            var resp3 = await controller.PutUser(userid, f);
            var resp4 = controller.GetUser(userid);
            Assert.IsTrue(resp4.Result.Value.username.Equals("Dave"));

        }
        [Test]
        public async Task test_deletion()
        { 
            var resp1 = await controller.PostUser(test_user);
            var resp2 = controller.GetUser(test_user.userId);
            Assert.IsTrue(resp2.Result.Value==test_user);
            controller.DeleteUser(test_user.userId);
            var resp3 = controller.GetUser(test_user.userId);
            Assert.IsTrue(resp3.Result.Value==null);

        }

    }
}