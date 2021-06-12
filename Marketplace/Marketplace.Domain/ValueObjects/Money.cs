using System;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Services;
using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.ValueObjects
{
    public class Money : Value<Money>
    {
        #region Fields

        public static string DefaultCurrencyCode = "EUR";

        #endregion

        #region Properties

        public CurrencyDetails Currency { get; }
        public decimal Amount { get; }

        #endregion

        #region Initializers

        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                throw new ArgumentException($"'{nameof(currencyCode)}' cannot be null or whitespace.",
                    nameof(currencyCode));
            }

            if (currencyLookup is null)
            {
                throw new ArgumentNullException(nameof(currencyLookup));
            }

            var currency = currencyLookup.FindCurrency(currencyCode);

            if (!currency.InUse)
            {
                throw new ArgumentException($"Currency {currencyCode} is not valid", nameof(currencyCode));
            }

            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount),
                    $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");
            }

            Amount = amount;
            Currency = currency;
        }

        private Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        #endregion

        #region Public Methods

        public static Money FromDecimal(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            return new(amount, currencyCode, currencyLookup);
        }

        public static Money FromString(string amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            return new(decimal.Parse(amount), currencyCode, currencyLookup);
        }

        public static Money operator +(Money summand1, Money summand2)
        {
            return summand1.Add(summand2);
        }

        public static Money operator -(Money minuend, Money subtrahend)
        {
            return minuend.Subtract(subtrahend);
        }

        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
            {
                throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
            }

            return new Money(Amount + summand.Amount, Currency);
        }

        public Money Subtract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }

            return new Money(Amount - subtrahend.Amount, Currency);
        }

        public override bool Equals(Money other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Amount.Equals(other.Amount) &&
                   Currency.Equals(other.Currency);
        }

        public override int HashCode()
        {
            return Amount.GetHashCode() + 
                   Currency.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Currency.CurrencyCode} {Amount}";
        }

        #endregion
    }
}