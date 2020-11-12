using Application.Services;
using Domain.Accounts;
using Domain.Accounts.Credits;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.OpenAccount
{
    public class OpenAccountUseCase : IOpenAccountUseCase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountFactory _accountFactory;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerService _customerService;
        private IOutputPort? _outputPort;

        public OpenAccountUseCase(
            ILogger<IOpenAccountUseCase> logger,
            IUnitOfWork unitOfWork,
            IAccountFactory accountFactory,
            IAccountRepository accountRepository,
            ICustomerService customerService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _accountFactory = accountFactory;
            _customerService = customerService;
        }

        public void SetOutputPort(IOutputPort outputPort) 
        {
            _outputPort = outputPort; 
        }

        public Task ExecuteAsync(decimal amount, string currency)
        {
            return OpenAccount(new Money(new Currency(currency), amount));
        }

        private async Task OpenAccount(Money amountToDeposit)
        {
            var customerId = _customerService.GetCurrentCustomerId();
            var account = _accountFactory.NewAccount(customerId, amountToDeposit.Currency);
            var credit = _accountFactory.NewCredit(account, amountToDeposit, DateTime.UtcNow);

            await Deposit(account, credit);

            _outputPort?.Ok(account);
        }

        private async Task Deposit(Account account, Credit credit)
        {
            account.Deposit(credit);

            await _accountRepository.Add(account, credit);

            await _unitOfWork.SaveAsync();
        }
    }
}
