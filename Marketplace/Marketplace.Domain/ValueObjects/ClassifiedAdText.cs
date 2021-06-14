using System;
using System.Text.RegularExpressions;
using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.ValueObjects
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        #region Fields

        private const int ValueMaxLength = 1000;

        private readonly string _value;

        #endregion

        #region Properties

        #endregion

        #region Initializers

        private ClassifiedAdText(string value)
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

            _value = value;
        }

        #endregion

        #region Public Methods

        public static ClassifiedAdText FromString(string title)
        {
            return new(title);
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

        public static implicit operator string(ClassifiedAdText classifiedAdText)
        {
            return classifiedAdText._value;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}