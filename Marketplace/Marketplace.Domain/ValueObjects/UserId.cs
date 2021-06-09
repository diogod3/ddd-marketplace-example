﻿using Marketplace.Framework.Helpers;
using System;

namespace Marketplace.Domain.ValueObjects
{
    public class UserId : Value<UserId>
    {
        #region Fields

        private readonly Guid _value;

        #endregion

        #region Initializers

        public UserId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentException("Identity must be specified", nameof(value));
            }

            _value = value;
        }

        public override bool Equals(UserId other)
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

        public override int HashCode() => _value.GetHashCode();

        #endregion
    }
}