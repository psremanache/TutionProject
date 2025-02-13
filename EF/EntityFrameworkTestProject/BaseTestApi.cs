using EntityFrameworkCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTestProject
{
    [SetUpFixture]
    public class BaseTestApi
    {
        protected static HttpClient _client;
        protected static string Token;

        [OneTimeSetUp] // Runs once before all tests
        public async Task GlobalSetup()
        {
            var factory = new CustomWebApplicationFactory();
            _client = factory.CreateClient();

            Token = await GetAuthToken();
        }
        private async Task<string> GetAuthToken()
        {
            var newUser = new LoginRequest { Username = "psremanache", Password = "psr@123" };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/Login/Login", jsonContent);
            response.EnsureSuccessStatusCode(); // Fails the test if the request is not successful

            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

            return tokenResponse?.Token;
        }

        [OneTimeTearDown] // Runs once after all tests
        public void GlobalTeardown()
        {
            _client.Dispose();
        }
        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
