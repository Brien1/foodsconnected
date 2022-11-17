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
          
            id_nonexistent_user = 1111;

        }
        [TearDown]
        public void TearDown() { 

        }

        [Test]
        public void test_get_set_user()
        {
            test_user.username = "lizzy";
            test_user.userId = 8;

            var resp_data_insertion = controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(test_user.userId);

            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("lizzy"));


        }

        [Test]
        public async Task test_get_set_user_sameNameError()
        {
            test_user.username = "brien";
            test_user.userId = 1;
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
            test_user.username = "brien";
            var resp_data_insertion = await controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(userid);
            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("brien"));
            // User f = resp_data_requested.Result.Value;
            User f = new User();
            f.userId = 2;
            f.username = "Moody";
            var resp_user_updated = await controller.PutUser(userid, "Moody");
            
            
            Assert.IsTrue(resp_user_updated.Value.username.Equals("Moody"));

        }
          [Test]
        public async Task test_alter_username_same_name_error()
        {
            test_user.userId = 4;
            long userid = test_user.userId;
            test_user.username = "brien";
            var resp_data_insertion = await controller.PostUser(test_user);
            var resp_data_requested = controller.GetUser(userid);
            Assert.IsTrue(resp_data_requested.Result.Value.username.Equals("brien"));
          
            var resp_user_updated = await controller.PutUser(userid, "brien");
            
            Assert.AreEqual(resp_user_updated.Result.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestResult" );

        }
        [Test]
        public async Task test_deletion()
        { 

            test_user.username = "dotty";
            test_user.userId = 3;
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
           
            var resp_user_updated = await controller.PutUser(5555, "fff");
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult",resp_user_updated.Result.ToString());

        }
        [Test]
        public async Task test_get_all_users() {

            var users = await controller.GetUser();
            var current_count = users.Value.Count<User>();
            var u1 = new User();
            var u2 = new User();
            u1.userId = 101;
            u2.userId = 102;
            u1.username = "b";
            u2.username = "bb";
            controller.PostUser(u1);
            controller.PostUser(u2);
            users = await controller.GetUser();
            Assert.True(users.Value.Count<User>()== current_count + 2);

        }
     
    }
}