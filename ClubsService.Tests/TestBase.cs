
using ClubsService.DB;
using ClubsService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ClubsService.Tests
{
    internal class TestBase : WebApplicationFactory<Program>
    {
        private readonly string _environment;
        public Guid _seedClubId;
        public string _seedClubName;
        public Member _seedMember1;
        public Member _seedMember2;
        public  ClubsDbContext _dbContext;
        public TestBase(string environment = "Development")
        {
            _environment = environment;
            _seedClubId = Guid.NewGuid();
            _seedClubName = "Seed Club";
            _seedMember1 = new Member { Id = 1, Name = "Test1" };
            _seedMember2 = new Member { Id = 2, Name = "Test2" };
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                services.AddScoped(sp =>
                {
                    // Replace SQLite with in-memory database for tests
                    return new DbContextOptionsBuilder<ClubsDbContext>()
                    .UseInMemoryDatabase("Tests")
                    .UseApplicationServiceProvider(sp)
                    .Options;
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    _dbContext = scopedServices.GetRequiredService<ClubsDbContext>();
                    CreateTestData();
                }
            });

            return base.CreateHost(builder);
        }

        private void CreateTestData()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Members.Add(_seedMember1);
            _dbContext.Members.Add(_seedMember2);
            _dbContext.Clubs.Add(new Club { Id = _seedClubId, Name = _seedClubName });
            _dbContext.SaveChanges();
        }
    }
}
