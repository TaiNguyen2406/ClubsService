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

namespace ClubsService.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task CreateClub()
        {
            var application = new TestBase();

            var client = application.CreateClient();
          
            var data = new CreateUpdateClubDto
            {
                Name = "TestClub Name 1"
            };
            using var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            content.Headers.Add("Player-Id", "1");
            var response = await client.PostAsync("/api/clubs", content);
            var resultClub = JsonSerializer.Deserialize<Club>(await response.Content.ReadAsStringAsync());
            Assert.Equals(data.Name, resultClub.Name);
        }
    }
}
