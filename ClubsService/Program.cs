using ClubsService.DB;
using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<ClubsDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"));
});
services.AddSqlRepositories();
services.AddHealthChecks();
//simple example for retry policy when calling to other service
var httpRetryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(retryAttempt));
services.AddSingleton<IAsyncPolicy<HttpResponseMessage>>(httpRetryPolicy);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.UseAuthorization();

app.MapControllers();

app.Run();
