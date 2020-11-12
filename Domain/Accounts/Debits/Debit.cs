using Domain.Accounts;
using Domain.ValueObjects;
using System;

namespace Domain.Accounts.Debits
{
    public class Debit
    {
        public static string Description => "Debit";

        public DebitId DebitId { get; }
        public Money Amount { get; }
        public string Currency => Amount.Currency.Code;
        public decimal Value => Amount.Amount;
        
        public DateTime TransactionDate { get; }
        public AccountId AccountId { get; }
        public Account Account { get; set; }        

        public Debit(DebitId debitId, AccountId accountId, DateTime transactionDate, decimal value, string currency)
        {
            DebitId = debitId;
            TransactionDate = transactionDate;            
            AccountId = accountId;
            Amount = new Money(new Currency(currency), value);
        }

        public Money Sum(Money amount)
        {
            return this.Amount.Add(amount);
        }
    }
}
