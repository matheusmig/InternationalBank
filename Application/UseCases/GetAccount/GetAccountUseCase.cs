using Domain.Accounts;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.GetAccount
{
    public sealed class GetAccountUseCase : IGetAccountUseCase
    {
        private readonly ILogger _logger;
        private readonly IAccountRepository _accountRepository;
        private IOutputPort? _outputPort;

        public GetAccountUseCase(
            ILogger<IGetAccountUseCase> logger,
            IAccountRepository accountRepository) 
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        public Task ExecuteAsync(Guid accountId)
        {
            return GetAccountInternal(new AccountId(accountId));
        }

        private async Task GetAccountInternal(AccountId accountId)
        {
            var account = await _accountRepository.Get(accountId);

            if (account is Account getAccount)
            {
                this._outputPort?.Ok(getAccount);
                return;
            }

            this._outputPort?.NotFound();
        }
    }
}
