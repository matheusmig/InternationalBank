using Domain.Accounts.Credits;
using Domain.Accounts.Debits;
using Domain.ValueObjects;

namespace Domain.Accounts
{
    public class Account : IAccount
    {
        public AccountId AccountId { get; set; }
        public CustomerId CustomerId { get; }
        public Currency Currency { get; set; }

        public CreditsCollection CreditsCollection { get; } = new CreditsCollection();
        public DebitsCollection DebitsCollection { get; } = new DebitsCollection();

        public Account(AccountId accountId, CustomerId customerId, Currency currency)
        {
            AccountId = accountId;
            CustomerId = customerId;
            Currency = currency;
        }

        public void Deposit(Credit credit) 
        {
            CreditsCollection.Add(credit);
        }

        public void Withdraw(Debit debit)
        {
            DebitsCollection.Add(debit);
        }

        public Money GetCurrentBalance()
        {
            Money totalCredits = CreditsCollection
                .GetTotal();

            Money totalDebits = DebitsCollection
                .GetTotal();

            Money totalAmount = totalCredits
                .Subtract(totalDebits);

            return totalAmount;
        }
    }
}
