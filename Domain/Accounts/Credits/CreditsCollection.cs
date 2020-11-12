using Domain.Accounts.Credits;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Accounts.Debits
{
    public sealed class CreditsCollection: List<Credit>
    {
        public Money GetTotal()
        {
            if (!this.Any())
                return new Money(new Currency(string.Empty), 0);

            Money total = new Money(this.First().Amount.Currency, 0);
            return this.Aggregate(total, (x, y) =>
            {
                if (x.Currency != total.Currency)
                    throw new Exception($"{nameof(CreditsCollection)} Cannot get total, there are differente currencies {x.Currency} -> {y.Currency}");

                return new Money(x.Currency, x.Amount + y.Amount.Amount);
            });
        }
    }
}
