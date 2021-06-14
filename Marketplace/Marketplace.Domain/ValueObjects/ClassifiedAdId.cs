﻿using Marketplace.Framework.Helpers;
using System;

namespace Marketplace.Domain.ValueObjects
{
    public class ClassifiedAdId : Value<ClassifiedAdId>
    {
        #region Fields

        private readonly Guid _value;

        #endregion

        #region Initializers

        public ClassifiedAdId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentException("Identity must be specified", nameof(value));
            }

            _value = value;
        }

        public static implicit operator Guid(ClassifiedAdId classifiedAdId)
        {
            return classifiedAdId._value;
        }

        public override bool Equals(ClassifiedAdId other)
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
    }
}