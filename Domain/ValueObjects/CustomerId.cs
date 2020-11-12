using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects
{
    public readonly struct CustomerId : IEquatable<CustomerId>
    {
        public Guid Id { get; }

        public CustomerId(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is CustomerId o && this.Equals(o);
        }

        public bool Equals([AllowNull] CustomerId other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(CustomerId left, CustomerId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CustomerId left, CustomerId right)
        {
            return !(left == right);
        }

        public override string ToString() 
        {
            return Id.ToString(); 
        }

    }
}
