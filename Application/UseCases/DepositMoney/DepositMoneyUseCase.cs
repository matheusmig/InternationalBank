using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Credits;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.DepositMoney
{
    public sealed class DepositMoneyUseCase : IDepositMoneyUseCase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountFactory _accountFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyExchanger _currencyExchanger;
        
        private IOutputPort? _outputPort;

        public DepositMoneyUseCase(
            ILogger<IDepositMoneyUseCase> logger,
            IUnitOfWork unitOfWork,
            IAccountRepository accountRepository,            
            IAccountFactory accountFactory,
            ICurrencyExchanger currencyExchanger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountFactory = accountFactory;
            _accountRepository = accountRepository;
            _currencyExchanger = currencyExchanger;            
        }

        public Task ExecuteAsync(Guid accountId, decimal amount, string currency)
        {
            return DepositAsync(
                new AccountId(accountId),
                new Money(new Currency(currency), amount));
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }

        private async Task DepositAsync(AccountId accountId, Money amount)
        {
            var account = await _accountRepository.Get(accountId);
            if (account is Account depositAccount)
            {
                var convertedAmount = await _currencyExchanger.Convert(amount, depositAccount.Currency);
                var credit = _accountFactory.NewCredit(depositAccount, convertedAmount, DateTime.UtcNow);

                await DepositAsync(depositAccount, credit);

                _outputPort?.Ok(credit, depositAccount);
                return;
            }

            _logger.LogWarning($"{nameof(DepositAsync)} Account not found {accountId}");
        }

        private async Task DepositAsync(Account account, Credit credit)
        {
            account.Deposit(credit);

            await _accountRepository.Update(account, credit);

            await _unitOfWork.SaveAsync();
        }
    }
}
