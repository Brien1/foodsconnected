
using foods_connected_brien.Controllers;
using TodoApi.Models;


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

            var resp_data_insertion = controller.PostUser(test_user.username);
            var resp_data_requested = controller.GetUser();

            Assert.AreEqual(resp_data_requested.Result.Value.Last().username , "lizzy");


        }

        [Test]
        public async Task test_get_set_user_sameNameError()
        {
            test_user.username = "brien";
            test_user.userId = 1;
            var resp_data_insertion = await controller.PostUser(test_user.username);
            var resp_data_insertion_dubplicate = await controller.PostUser(test_user.username);
            var r = resp_data_insertion_dubplicate.Result.ToString();
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundObjectResult", r);

        }

        [Test]
        public async Task test_alter_username()
        {
          
            test_user.username = "brien";
            var resp_data_insertion = await controller.PostUser(test_user.username);
            var resp_data_requested = controller.GetUser();
            Assert.AreEqual(resp_data_requested.Result.Value.ElementAt(0).username , "brien")   ;
            var resp_user_updated = await controller.PutUser(resp_data_requested.Result.Value.ElementAt(0).userId, "Moody");
            resp_data_requested = controller.GetUser();
            Assert.AreEqual(resp_data_requested.Result.Value.ElementAt(0).username , "Moody");

        }
          [Test]
        public async Task test_alter_username_same_name_error()
        {

            long userid = 4;
            test_user.username = "brien";
            var resp_data_insertion = await controller.PostUser(test_user.username);
            var resp_data_requested = controller.GetUser();
            Assert.AreEqual(resp_data_requested.Result.Value.Last().username , "brien");
          
            var resp_user_updated = await controller.PutUser(resp_data_requested.Result.Value.Last().userId, "brien");
            
            Assert.AreEqual(resp_user_updated.Result.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestObjectResult" );

        }
        [Test]
        public async Task test_deletion()
        { 

            await controller.PostUser("non-null");
            test_user.username = "dotty";
            var resp_data_insertion = await controller.PostUser(test_user.username);

            var resp_data_requested = controller.GetUser();
            var last_insert = resp_data_requested.Result.Value.Last();
            Assert.AreEqual(last_insert.username , "dotty");
            await controller.DeleteUser(resp_data_requested.Result.Value.Last().userId);
            var resp_data_requested_afterDelete = controller.GetUser();
            var last_insert_after_delete = resp_data_requested_afterDelete.Result.Value.Last();

            Assert.IsTrue(last_insert_after_delete.userId != last_insert.userId);
            Assert.IsTrue(last_insert_after_delete.username != last_insert.username);


        }

        [Test]
        public async Task test_deletion_user_doesnt_exist()
        {
                        
            var resp = controller.DeleteUser(id_nonexistent_user);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", resp.Result.Result.ToString());
        }
        [Test]
        public async Task test_put_user_wrong_id() {
           
            var resp_user_updated = await controller.PutUser(5555, "fff");
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundObjectResult",resp_user_updated.Result.ToString());

        }
        [Test]
        public async Task test_get_all_users() {

            var users = await controller.GetUser();
            var current_count = users.Value.Count<User>();
            controller.PostUser("b");
            controller.PostUser("bb");
            users = await controller.GetUser();
            Assert.True(users.Value.Count<User>()== current_count + 2);

        }
     
    }
}