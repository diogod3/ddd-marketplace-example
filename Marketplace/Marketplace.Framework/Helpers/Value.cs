using System;

namespace Marketplace.Framework.Helpers
{
    public abstract class Value<T> : IEquatable<T>
    {
        #region Initializers

        public Value()
        { }

        #endregion

        #region Public Methods

        public abstract bool Equals(T other);

        public abstract int HashCode();

        public static bool operator ==(Value<T> left, Value<T> right) => Equals(left, right);

        public static bool operator !=(Value<T> left, Value<T> right) => !Equals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((T)obj);
        }

        public override int GetHashCode()
        {
            return HashCode();
        }

        #endregion
    }
}