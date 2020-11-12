using Application.Services;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.OpenAccount
{
    public sealed class OpenAccountValidationUseCases : IOpenAccountUseCase
    {
        private readonly IOpenAccountUseCase _useCase;
        private IOutputPort? _outputPort;

        public OpenAccountValidationUseCases(IOpenAccountUseCase useCase)
        {
            _useCase = useCase;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }

        public Task ExecuteAsync(decimal amount, string currency)
        {
            var modelState = new ApplicationResult();

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
                return _useCase.ExecuteAsync(amount, currency);

            _outputPort?.Invalid(modelState);

            return Task.CompletedTask;
        }        
    }
}
