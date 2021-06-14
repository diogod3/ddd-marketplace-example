using System;
using Marketplace.Domain.Services;

namespace Marketplace.Domain.ValueObjects
{
    public class Price : Money
    {
        private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
            : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }

        internal Price(decimal amount, string currencyCode)
            : base(amount, new CurrencyDetails {CurrencyCode = currencyCode})
        {
        }


        public Price(decimal amount) : base(amount, "EUR", null)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }

        public new static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup)
        {
            return new(amount, currency, currencyLookup);
        }
    }
}