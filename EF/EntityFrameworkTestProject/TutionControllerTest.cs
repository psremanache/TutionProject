using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTestProject
{
    [TestFixture]
    public class TutionControllerTest:BaseTestApi
    {
        [Test]
        public async Task GetAllCourses_ReturnsOk()
        {
            // Act
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var response = await _client.GetAsync("/api/Tution/GetAllCourses");

            // Assert
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }
    }
}
