using System;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities
{
    /// <summary>
    /// </summary>
    public class ClassifiedAdd
    {
        #region Initializers

        /// <summary>Initializes a new instance of the <see cref="ClassifiedAdd" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <exception cref="ArgumentNullException">
        ///     id
        ///     or
        ///     ownerId
        /// </exception>
        public ClassifiedAdd(ClassifiedAddId id, UserId ownerId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            _ownerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        }

        #endregion

        #region Properties

        /// <summary>Gets the identifier.</summary>
        public ClassifiedAddId Id { get; }

        #endregion

        #region Fields

        /// <summary>The owner identifier</summary>
        private UserId _ownerId;

        /// <summary>The title</summary>
        private string _title;

        /// <summary>The text</summary>
        private string _text;

        /// <summary>The price</summary>
        private decimal _price;

        #endregion

        #region Public Methods

        /// <summary>Sets the title.</summary>
        /// <param name="title">The title.</param>
        /// <exception cref="ArgumentException">'{nameof(title)}' cannot be null or whitespace. - title</exception>
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));

            _title = title;
        }

        /// <summary>Updates the text.</summary>
        /// <param name="text">The text.</param>
        /// <exception cref="ArgumentException">'{nameof(text)}' cannot be null or whitespace. - text</exception>
        public void UpdateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException($"'{nameof(text)}' cannot be null or whitespace.", nameof(text));

            _text = text;
        }

        /// <summary>Updates the price.</summary>
        /// <param name="price">The price.</param>
        public void UpdatePrice(decimal price)
        {
            _price = price;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}