using Application.Services;
using Infrastructure.CurrencyExchanger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    public static class CurrencyExchangeExtensions
    {
        public static IServiceCollection AddCurrencyExchange(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddScoped<ICurrencyExchanger, CurrencyExchanger>();

            return services;
        }
    }
}
