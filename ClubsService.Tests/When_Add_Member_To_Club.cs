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
    public class When_Add_Member_To_Club : IDisposable
    {
        private HttpClient _client;
        private TestBase _application;
        public When_Add_Member_To_Club()
        {
            _application = new TestBase();
            _client = _application.CreateClient();
        }
        public void Dispose()
        {
            _application.Dispose();
        }
        [TestMethod]
        public async Task Then_The_Member_Should_Be_Added()
        {
            var data = new AddMemberToClubDto
            {
                PlayerId = 2,
            };
            var response = await AddMemberToClub(data,_application._seedClubId);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            var getResponse = await _client.GetAsync($"/api/clubs/{_application._seedClubId}");
            var responseBody = await getResponse.Content.ReadAsStringAsync();
            var resultClub = JsonSerializer.Deserialize<Club>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            resultClub.Members.Select(x=>x.Id).Should().ContainEquivalentOf(_application._seedMember2.Id);
        }

        private async  Task<HttpResponseMessage> AddMemberToClub(AddMemberToClubDto data, Guid clubId)
        {
            using var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = await _client.PostAsync($"/api/clubs/{clubId}/members", content);
            return response;
        }
    }
}
