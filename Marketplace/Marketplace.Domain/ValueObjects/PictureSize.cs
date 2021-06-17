using Marketplace.Framework.Helpers;
using System;

namespace Marketplace.Domain.ValueObjects
{
    public class PictureSize : Value<PictureSize>
    {
        #region Properties

        public int Width { get; internal set; }
        public int Height { get; internal set; }

        #endregion

        #region Initializers

        public PictureSize(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Picture width must be a positive number");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Picture height must be a positive number");
            }

            Width = width;
            Height = height;
        }

        internal PictureSize(){ }

        public override bool Equals(PictureSize other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public override int HashCode()
        {
            return Width.GetHashCode() + Height.GetHashCode();
        }

        #endregion
    }
}