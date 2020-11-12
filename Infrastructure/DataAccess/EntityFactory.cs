using Domain.Accounts;
using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;
using System;

namespace Infrastructure.DataAccess
{
    public sealed class EntityFactory : IAccountFactory
    {
        public Account NewAccount(CustomerId customerId, Currency currency)
        {
            return new Account(new AccountId(Guid.NewGuid()), customerId, currency);
        }

        public Credit NewCredit(Account account, Money toDeposit, DateTime transactionDate)
        {
            return new Credit(
                new CreditId(Guid.NewGuid()),
                account.AccountId,
                transactionDate,
                toDeposit.Amount,
                toDeposit.Currency.Code
                );
        }

        public Debit NewDebit(Account account, Money toWithdraw, DateTime transactionDate)
        {
            return new Debit(
                new DebitId(Guid.NewGuid()),
                account.AccountId,
                transactionDate,
                toWithdraw.Amount,
                toWithdraw.Currency.Code
                );
        }
    }
}
