using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.ValueObjects
{
    public readonly struct DebitId : IEquatable<DebitId>
    {
        public Guid Id { get; }

        public DebitId(Guid id)
        {
            Id = id;
        }

        public override bool Equals(object? obj)
        {
            return obj is DebitId o && this.Equals(o);
        }

        public bool Equals([AllowNull] DebitId other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(DebitId left, DebitId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DebitId left, DebitId right)
        {
            return !(left == right);
        }

        public override string ToString() 
        {
            return Id.ToString(); 
        }

    }
}
