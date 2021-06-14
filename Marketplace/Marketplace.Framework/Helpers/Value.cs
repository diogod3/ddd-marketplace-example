using System;

namespace Marketplace.Framework.Helpers
{
    public abstract class Value<T> : IEquatable<T>
    {
        #region Public Methods

        public static bool operator ==(Value<T> left, Value<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Value<T> left, Value<T> right)
        {
            return !Equals(left, right);
        }

        public abstract bool Equals(T other);

        public abstract int HashCode();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            return HashCode();
        }

        #endregion
    }
}