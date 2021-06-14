using Marketplace.Framework.Helpers;
using System;

namespace Marketplace.Domain.ValueObjects
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        #region Fields

        private const int ValueMaxLength = 1000;

        private readonly string _value;

        #endregion

        #region Initializers

        internal ClassifiedAdText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            _value = value;
        }

        #endregion

        #region Public Methods

        public static ClassifiedAdText FromString(string title)
        {
            CheckValidity(title);

            return new(title);
        }

        public static implicit operator string(ClassifiedAdText classifiedAdText)
        {
            return classifiedAdText._value;
        }

        public override bool Equals(ClassifiedAdText other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _value.Equals(other._value);
        }

        public override int HashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region Private Methods

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            if (value.Length > ValueMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Text cannot be longer than {ValueMaxLength} characters");
            }
        }

        #endregion
    }
}