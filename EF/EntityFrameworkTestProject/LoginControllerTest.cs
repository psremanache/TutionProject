using EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;

namespace EntityFrameworkTestProject
{
    public class UserTestData
    {
        public static IEnumerable<TestCaseData> Users()
        {
            yield return new TestCaseData(new User { Username = "psremanache",Password="psr@123",RoleId=1 });
            yield return new TestCaseData(new User { Username = "psremanache", Password = "psr@123", RoleId = 10 });
            yield return new TestCaseData(new User { Username = "psremanache", Password = "psr@123", RoleId = 2 });
        }
    }
    [TestFixture]
    public class LoginControllerTest: BaseTestApi
    {
        
        [Test]
        [TestCase("psremanache", "psr@123",200)]
        [TestCase("psr", "psr@123",401)]
        public async Task Login_ExistingUser_ReturnsOk(string userName,string password,int expectedStatusCode)
        {
            // Act
            var newUser = new LoginRequest { Username = userName, Password=password };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Login", jsonContent);

            // Assert
            Assert.That((int)response.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(response.Content, Is.Not.Null);
        }

        [Test]
        [TestCaseSource(typeof(UserTestData), nameof(UserTestData.Users))]
        public async Task Create_NewUser_ReturnsOk(User newUser)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Register", jsonContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(responseBody);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That((int)response.StatusCode, Is.EqualTo(200), "Expected status code 200 Created");
                Assert.IsNotNull(user, "Created user should not be null");
                Assert.That(user.Username, Is.EqualTo("psremanache"), "User name should match");
            });
        }
    }
}