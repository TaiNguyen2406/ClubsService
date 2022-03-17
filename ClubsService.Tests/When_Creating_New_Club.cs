using ClubsService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClubsService.Models;
using Xunit;
using ClubsService.DB;

namespace ClubsService.Tests
{
    [TestClass]
    public class When_Creating_New_Club : IDisposable
    {
        private HttpClient _client;
        private TestBase _application;
        public When_Creating_New_Club()
        {
            _application = new TestBase();
            _client = _application.CreateClient();
        }
        public void Dispose()
        {
            _application.Dispose();
        }
        [TestMethod]
        public async Task Then_The_New_Club_Should_Be_Returned()
        {
            var data = new CreateUpdateClubDto
            {
                Name = "TestClub Name 1"
            };
            var response = await CreateClub(data);
            var responseBody = await response.Content.ReadAsStringAsync();
            var resultClub = JsonSerializer.Deserialize<Club>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            resultClub.Name.Should().Be(data.Name);
        }

        [TestMethod]
        public async Task With_Duplicate_Name_Then_Conflict_Response_Should_Be_Returned()
        {
            var data = new CreateUpdateClubDto
            {
                Name = _application._seedClubName
            };
            var response = await CreateClub(data);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict);
        }

        public async  Task<HttpResponseMessage> CreateClub(CreateUpdateClubDto data)
        {
            using var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            content.Headers.Add("Player-Id", "1");
            var response = await _client.PostAsync("/api/clubs", content).ConfigureAwait(false);
            return response;
        }
    }
}
