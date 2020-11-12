using Application.Services;
using Domain.Accounts;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApi.Modules
{
    public static class MySqlExtesions
    {
        public static IServiceCollection AddMySql(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<InternationalBankContext>(
                options => {
                    options.UseMySql(configuration.GetValue<string>("PersistenceModule:DefaultConnection"),
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(1), errorNumbersToAdd: null);
                    }); 
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IAccountFactory, EntityFactory>();

            return services;
        }
    }
}
