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
        private long id_nonexistent_user;

        [SetUp]
        public void SetUp()
        {

            controller = new UserController();
            test_user = new User();
            test_user.username = "brien";
            id_nonexistent_user = 1111;

        }
        [TearDown]
        public void TearDown() { }

        [Test]
        public void test_get_set_user()
        {

            test_user.userId = 1;
            var resp_data_insertion = controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(1);

            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("brien"));


        }

        [Test]
        public async Task test_get_set_user_sameNameError()
        {
            var resp_data_insertion = await controller.PostUser(test_user);
            var resp_data_insertion_dubplicate = await controller.PostUser(test_user);
            var r = resp_data_insertion_dubplicate.Result.ToString();
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.BadRequestObjectResult", r);

        }

        [Test]
        public async Task test_alter_username()
        {
            test_user.userId = 2;
            long userid = test_user.userId;

            var resp_data_insertion = await controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(userid);
            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("brien"));
            User f = resp_data_requested.Result.Value;
            f.username = "Dave";
            var resp_user_updated = await controller.PutUser(userid, f);
            
            resp_data_requested = controller.GetUser(userid);
            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("Dave"));

        }
        [Test]
        public async Task test_deletion()
        { 
            var resp_data_insertion = await controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(test_user.userId);
            Assert.IsTrue(resp_data_requested.Result.Value==test_user);
            controller.DeleteUser(test_user.userId);
            var resp_data_requested_afterDelete = controller.GetUser(test_user.userId);
            Assert.IsTrue(resp_data_requested_afterDelete.Result.Value==null);

        }

        [Test]
        public async Task test_deletion_user_doesnt_exist()
        {
                        
            var resp = controller.DeleteUser(id_nonexistent_user);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", resp.Result.ToString());
        }
        [Test]
        public async Task test_put_user_wrong_id() {
           
            var resp_user_updated = await controller.PutUser(3, test_user);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.BadRequestResult",resp_user_updated.ToString());

        }
     
    }
}