using System.Collections.Generic;
using System.Linq;
using Marketplace.Domain.Services;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Services
{
    public class CurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> Currencies = new[]
        {
            new CurrencyDetails
            {
                CurrencyCode = "EUR",
                DecimalPlaces = 2,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "USD",
                DecimalPlaces = 2,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "JPY",
                DecimalPlaces = 0,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "DEM",
                DecimalPlaces = 2,
                InUse = false
            },
        };

        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = Currencies.FirstOrDefault(t => t.CurrencyCode == currencyCode);

            return currency ?? CurrencyDetails.None;
        }
    }
}