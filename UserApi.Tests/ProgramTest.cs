using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using foods_connected_brien.Controllers;
using TodoApi.Models;
using UserApi.ProgramMain;
using Microsoft.AspNetCore.Builder;
namespace UserApiTest.UnitTests.Program_Test
{
    [TestFixture]
    public class ProgramTest
    {
        private UserController usercontroller;
        private WebApplication app;
        [SetUp]
        public void SetUp()
        {
            string[] args = new String[] {};
            app = Program.create(args);
        }

        [Test]
        public void app_created()
        {

           Assert.IsTrue(app!=null);
        }
    }
}