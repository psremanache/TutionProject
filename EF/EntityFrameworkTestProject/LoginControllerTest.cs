using EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;

namespace EntityFrameworkTestProject
{
    [TestFixture]
    public class LoginControllerTest: BaseTestApi
    {
        
        [Test]
        public async Task Login_ExistingUser_ReturnsOk()
        {
            // Act
            var newUser = new LoginRequest { Username = "psremanache",Password="psr@123" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Login", jsonContent);

            // Assert
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Content, Is.Not.Null);
        }

        [Test]
        public async Task Create_NewUser_ReturnsOk()
        {
            var newUser = new User { Username = "psremanache", Password = "psr@123", RoleId = 2 };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Register", jsonContent);

            // Assert
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }
    }
}