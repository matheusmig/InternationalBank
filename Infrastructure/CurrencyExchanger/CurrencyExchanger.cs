using Application.Services;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.CurrencyExchanger
{
    public sealed class CurrencyExchanger : ICurrencyExchanger
    {
        private readonly Dictionary<Currency, decimal> _usdRates = new Dictionary<Currency, decimal>
        {
            {Currency.Dollar, 1m},
            {Currency.Euro, 0.80m},
            {Currency.Real, 5.50m},
            {Currency.MexicanPeso, 20.00m}
        };

        public async Task<Money> Convert(Money from, Currency to)
        {
            decimal usdAmount = _usdRates[from.Currency] / from.Amount;
            decimal destinatiomAmount = _usdRates[to] / usdAmount;

            return new Money(to, destinatiomAmount);
        }
    }
}
