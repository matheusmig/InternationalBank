using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GetAccount
{
    public sealed class GetAccountValidationUseCase : IGetAccountUseCase
    {
        private readonly IGetAccountUseCase _useCase;
        private IOutputPort? _outputPort;

        public GetAccountValidationUseCase(IGetAccountUseCase useCase)
        {
            _useCase = useCase;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
            _useCase.SetOutputPort(outputPort);
        }

        public Task ExecuteAsync(Guid accountId)
        {
            var modelState = new ApplicationResult();

            if (accountId == Guid.Empty)
            {
                modelState.Add(nameof(accountId), "AccountId is required.");
            }

            if (modelState.IsValid)
            {
                return _useCase.ExecuteAsync(accountId);
            }

            _outputPort?.Invalid(modelState);

            return Task.CompletedTask;
        }
    }
}
