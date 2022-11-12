using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using foods_connected_brien.Controllers;
using TodoApi.Models;
using Microsoft.AspNetCore.Builder;
using Moq;
namespace UserApiTest.UnitTests.User_Controller_Test
{
    [TestFixture]
    public class UserControllerTest
    {
  

        Mock<UserContext> usercontext;
            public void SetUp()
        {   
                usercontext = new();
                // mockRepo.Setup();
                var controller = new UserController(usercontext.Object);

            
        }

        [Test]
        public void test_user_controlled_created()
        {

        }

    }
}