using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects
{
    public readonly struct Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public override bool Equals(object? obj)
        {
            return obj is Money o && this.Equals(o);
        }

        public bool Equals([AllowNull] Money other)
        {
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Amount, this.Currency);
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        public Money Subtract(Money debit)
        {
            if (Currency != debit.Currency)
                throw new Exception($"Cannot subtract different currencies {debit.Currency} -> {Currency}");

            return new Money(Currency, Math.Round(Amount - debit.Amount, 2));
        }

        public Money Add(Money amount)
        {
            if (Currency != amount.Currency)
                throw new Exception($"Cannot add different currencies {amount.Currency} -> {Currency}");

            return new Money(Currency, Math.Round(Amount + amount.Amount, 2));
        }

        public override string ToString() 
        {
            return string.Format($"{Amount} {Currency}");
        }
    }
}
