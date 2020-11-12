using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Debits;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.WithdrawMoney
{
    public class WithdrawMoneyUseCase : IWithdrawMoneyUseCase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountFactory _accountFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyExchanger _currencyExchanger;
        private readonly ICustomerService _customerService;
        private IOutputPort? _outputPort;

        public WithdrawMoneyUseCase(
            ILogger<IWithdrawMoneyUseCase> logger,
            IUnitOfWork unitOfWork,
            IAccountFactory accountFactory,
            IAccountRepository accountRepository,
            ICurrencyExchanger currencyExchanger,
            ICustomerService customerService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _customerService = customerService;
            _currencyExchanger = currencyExchanger;
        }

        public void SetOutputPort(IOutputPort outputPort) 
        {
            _outputPort = outputPort; 
        }

        public Task ExecuteAsync(Guid accountId, decimal amount, string currency)
        {
            return Withdraw(
                new AccountId(accountId), 
                new Money(new Currency(currency), amount));
        }

        private async Task Withdraw(AccountId accountId, Money withdrawAmount)
        {
            var customerId = _customerService.GetCurrentCustomerId();

            IAccount account = await _accountRepository.Find(accountId, customerId);

            if (account is Account withdrawAccount)
            {
                Money localCurrencyAmount = await _currencyExchanger.Convert(withdrawAmount, withdrawAccount.Currency);
                Debit debit = _accountFactory.NewDebit(withdrawAccount, localCurrencyAmount, DateTime.UtcNow);

                var currentBalance = withdrawAccount.GetCurrentBalance();
                if (currentBalance.Subtract(debit.Amount).Amount < 0)
                {
                    _logger.LogWarning($"{nameof(Withdraw)} Account {accountId} balance insufficient");
                    _outputPort?.OutOfFunds();
                    return;
                }

                await Withdraw(withdrawAccount, debit);

                _outputPort?.Ok(debit, withdrawAccount);
                return;
            }

            _logger.LogWarning($"{nameof(Withdraw)} Account {accountId} not found for customer {customerId}");
            _outputPort?.NotFound();
        }

        private async Task Withdraw(Account account, Debit debit)
        {
            account.Withdraw(debit);

            await _accountRepository.Update(account, debit);

            await _unitOfWork.SaveAsync();
        }

    }
}
