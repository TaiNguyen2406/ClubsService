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
    public class When_Getting_Club : IDisposable
    {
        private HttpClient _client;
        private TestBase _application;
        public When_Getting_Club()
        {
            _application = new TestBase();
            _client = _application.CreateClient();
        }

        public void Dispose()
        {
            _application.Dispose();
        }

        [TestMethod]
        public async Task Then_The_Correct_Club_Should_Be_Returned()
        {
            var response = await GetClub(_application._seedClubId);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            var resultClub = JsonSerializer.Deserialize<Club>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            resultClub.Name.Should().Be(_application._seedClubName);
        }

        [TestMethod]
        public async Task With_Wrong_Id_Then_NotFound_Response_Should_Be_Returned()
        {
            var notFoundId = Guid.NewGuid();
            while(notFoundId == _application._seedClubId)
            {
                notFoundId = Guid.NewGuid();
            }
            var response = await GetClub(notFoundId);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        private async  Task<HttpResponseMessage> GetClub(Guid id)
        {
            var response = await _client.GetAsync($"/api/clubs/{id}").ConfigureAwait(false);
            return response;
        }
    }
}
