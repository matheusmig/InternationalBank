using Application.Services;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.DepositMoney
{
    public sealed class DepositMoneyValidationUseCase : IDepositMoneyUseCase
    {
        private readonly IDepositMoneyUseCase _useCase;
        private IOutputPort? _outputPort;

        public DepositMoneyValidationUseCase(IDepositMoneyUseCase useCase)
        {
            _useCase = useCase;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }

        public Task ExecuteAsync(Guid accountId, decimal amount, string currency)
        {
            var modelState = new ApplicationResult();

            if (accountId == Guid.Empty)
                modelState.Add(nameof(accountId), "AccountId is required.");

            if (currency != Currency.Dollar.Code &&
                currency != Currency.Euro.Code &&
                currency != Currency.Real.Code &&
                currency != Currency.MexicanPeso.Code)
            {
                modelState.Add(nameof(currency), $"Currency {currency} is invalid.");
            }

            if (amount <= 0)
            {
                modelState.Add(nameof(amount), "Amount should be positive and greather than zero.");
            }

            if (modelState.IsValid)
                return _useCase.ExecuteAsync(accountId, amount, currency);

            _outputPort?.Invalid(modelState);

            return Task.CompletedTask;
        }        
    }
}
