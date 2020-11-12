using Application.Services;
using Application.UseCases.DepositMoney;
using Application.UseCases.GetAccount;
using Application.UseCases.WithdrawMoney;
using Infrastructure.ExternalAuthentication;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    public static class UseCaseExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IGetAccountUseCase, GetAccountUseCase>();
            services.Decorate<IGetAccountUseCase, GetAccountValidationUseCase>();

            services.AddScoped<IDepositMoneyUseCase, DepositMoneyUseCase>();
            services.Decorate<IDepositMoneyUseCase, DepositMoneyValidationUseCase>();

            services.AddScoped<IWithdrawMoneyUseCase, WithdrawMoneyUseCase>();
            services.Decorate<IWithdrawMoneyUseCase, WithdrawMoneyValidationUseCase>();

            return services;
        }
    }
}
