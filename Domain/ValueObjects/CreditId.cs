using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects
{
    public readonly struct CreditId : IEquatable<CreditId>
    {
        public Guid Id { get; }

        public CreditId(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is CreditId o && this.Equals(o);
        }

        public bool Equals([AllowNull] CreditId other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(CreditId left, CreditId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CreditId left, CreditId right)
        {
            return !(left == right);
        }

        public override string ToString() 
        {
            return Id.ToString(); 
        }

    }
}
