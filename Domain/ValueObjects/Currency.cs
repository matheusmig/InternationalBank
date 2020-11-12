using System;

namespace Domain.ValueObjects
{
    public readonly struct Currency : IEquatable<Currency>
    {
        public string Code { get; }


        public Currency(string code)
        {
            Code = code;
        }


        public override bool Equals(object? obj)
        {
            return obj is Currency o && this.Equals(o);
        }

        public bool Equals(Currency other) 
        {
            return Code == other.Code;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code);
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Currency left, Currency right) 
        {
            return !(left == right); 
        }

        /// <summary>
        ///     Dollar.
        /// </summary>
        /// <returns>Currency.</returns>
        public static readonly Currency Dollar = new Currency("USD");

        /// <summary>
        ///     Euro.
        /// </summary>
        /// <returns>Currency.</returns>
        public static readonly Currency Euro = new Currency("EUR");

        /// <summary>
        ///     British Pound.
        /// </summary>
        /// <returns>Currency.</returns>
        public static readonly Currency MexicanPeso = new Currency("MXN");

        /// <summary>
        ///     Brazilian Real.
        /// </summary>
        /// <returns>Currency.</returns>
        public static readonly Currency Real = new Currency("BRL");

        public override string ToString() 
        {
            return this.Code; 
        }
    }
}
