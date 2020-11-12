using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;

namespace Domain.Accounts
{
    public interface IAccount
    {
        AccountId AccountId { get; }
        void Deposit(Credit credit);
        void Withdraw(Debit debit);
        Money GetCurrentBalance();
    }
}
