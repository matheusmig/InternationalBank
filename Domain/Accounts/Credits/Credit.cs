using Domain.Accounts;
using Domain.ValueObjects;
using System;

namespace Domain.Accounts.Credits
{
    public class Credit
    {
        public static string Description => "Credit";

        public CreditId CreditId { get; }

        public Money Amount { get; }
        public string Currency => Amount.Currency.Code;
        public decimal Value => Amount.Amount;

        public DateTime TransactionDate { get; }
        public AccountId AccountId { get; }
        public Account Account { get; set; }

        public Credit(CreditId creditId, AccountId accountId, DateTime transactionDate, decimal value, string currency)
        {
            CreditId = creditId;
            TransactionDate = transactionDate;
            AccountId = accountId;
            Amount = new Money(new Currency(currency), value);
        }

        public Money Sum(Money amount)
        {
            return Amount.Add(amount);
        }
    }
}
