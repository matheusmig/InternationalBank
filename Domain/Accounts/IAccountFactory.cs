using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;
using System;

namespace Domain.Accounts
{
    public interface IAccountFactory
    {
        Account NewAccount(CustomerId customerId, Currency currency);
        Credit NewCredit(Account account, Money toDeposit, DateTime transactionDate);
        Debit NewDebit(Account account, Money toWithdraw, DateTime transactionDate);
    }
}
