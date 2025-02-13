using EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;

namespace EntityFrameworkTestProject
{
    [TestFixture]
    public class LoginControllerTest
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new CustomWebApplicationFactory();
            _client = factory.CreateClient();
        }
        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
        [Test]
        public async Task Login_ExistingUser_ReturnsOk()
        {
            // Act
            var newUser = new User { Username = "psremanache",Password="psr@123" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Login", jsonContent);

            // Assert
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public async Task GetUser_NonExistingUser_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/users/99");

            Assert.AreEqual(404, (int)response.StatusCode);
        }
    }
}