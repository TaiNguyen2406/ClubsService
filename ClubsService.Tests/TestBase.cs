
using ClubsService.DB;
using ClubsService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClubsService.Tests
{
    internal class TestBase : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public TestBase(string environment = "Development")
        {
            _environment = environment;
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
                    var db = scopedServices.GetRequiredService<ClubsDbContext>();
                    if (db.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                        db.Database.Migrate();
                    CreateTestData(db);
                }
            });
          
            return base.CreateHost(builder);
        }

        private void CreateTestData(ClubsDbContext clubsDbContext)
        {
            clubsDbContext.Members.Add(new Member { Id = 1, Name = "Test1" });
            clubsDbContext.Members.Add(new Member { Id = 2, Name = "Test2" });
        }
    }
}
