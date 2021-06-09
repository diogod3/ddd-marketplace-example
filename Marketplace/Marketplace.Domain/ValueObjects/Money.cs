using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.ValueObjects
{
    public class Money : Value<Money>
    {
        #region Properties

        public decimal Amount { get; }

        #endregion

        #region Initializers

        public Money(decimal amount)
        {
            Amount = amount;
        }

        #endregion

        #region Public Methods

        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);

        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

        public Money Add(Money summand) => new(Amount + summand.Amount);

        public Money Subtract(Money subtrahend) => new(Amount - subtrahend.Amount);

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

            return Amount.Equals(other.Amount);
        }

        public override int HashCode() => Amount.GetHashCode();

        #endregion
    }
}