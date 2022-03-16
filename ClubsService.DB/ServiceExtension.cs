using ClubsService.DB.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsService.DB
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddSqlRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IClubRepository, ClubRepository>()
                .AddScoped<IMemberRepository, MemberRepository>();
            return services;
        }
    }
}
