using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects
{
    public readonly struct AccountId : IEquatable<AccountId>
    {
        public Guid Id { get; }

        public AccountId(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is AccountId o && this.Equals(o);
        }

        public bool Equals([AllowNull] AccountId other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(AccountId left, AccountId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AccountId left, AccountId right)
        {
            return !(left == right);
        }

        public override string ToString() 
        {
            return Id.ToString(); 
        }

    }
}
