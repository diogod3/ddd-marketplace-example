using System;

namespace Marketplace.Domain.ValueObjects
{
    public class Price : Money
    {
        public Price(decimal amount) : base(amount, "EUR", null)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(amount));
            }
        }
    }
}