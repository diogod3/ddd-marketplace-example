using System;
using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.ValueObjects
{
    public class CurrencyDetails : Value<CurrencyDetails>
    {
        #region Fields

        #endregion

        #region Properties

        public string CurrencyCode { get; set; }
        public bool InUse { get; set; }
        public int DecimalPlaces { get; set; }

        public static CurrencyDetails None = new() {InUse = false};

        #endregion

        #region Initializers

        public override bool Equals(CurrencyDetails other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return CurrencyCode.Equals(other.CurrencyCode) &&
                   InUse.Equals(other.InUse) &&
                   DecimalPlaces.Equals(other.DecimalPlaces);
        }

        public override int HashCode()
        {
            return CurrencyCode.GetHashCode() + 
                   InUse.GetHashCode() + 
                   DecimalPlaces.GetHashCode();
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}