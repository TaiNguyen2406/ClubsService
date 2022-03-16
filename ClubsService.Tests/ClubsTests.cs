using ClubsService.Controllers;
using ClubsService.DB.Repository;
using ClubsService.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.Tests
{
    public class ClubsTests
    {
    //    [Fact]
    //    public async Task When_Getting_Club_With_Id_Should_Return_Correct_Club()
    //    {
    //        // Arrange
    //       var testClubId = Guid.NewGuid();
    //        var mockClubRepo = new Mock<ClubRepository>();
    //        mockClubRepo.Setup(repo => repo.GetClub(testClubId)).Returns(GetTestClubs().FirstOrDefault());

    //        var mockMemberRepo = new Mock<MemberRepository>();
    //        mockMemberRepo.Setup(repo => repo.GetClub(testClubId)).Returns(GetTestClubs().FirstOrDefault());
    //        var controller = new ClubsController(mockClubRepo.Object);

    //        // Act
    //        var result = await controller.Index(testSessionId);

    //        // Assert
    //        var contentResult = Assert.IsType<ContentResult>(result);
    //        Assert.Equal("Session not found.", contentResult.Content);
    //    }

    //    private List<Club> GetTestClubs()
    //    {
    //        var clubs = new List<Club>();
    //        clubs.Add(new Club()
    //        {
    //            Id = Guid.NewGuid(),
    //            Name = "Test One"
    //        });
    //        clubs.Add(new Club()
    //        {
    //            Id = Guid.NewGuid(),
    //            Name = "Test Two"
    //        });
    //        return clubs;
    //    }
    }
}
