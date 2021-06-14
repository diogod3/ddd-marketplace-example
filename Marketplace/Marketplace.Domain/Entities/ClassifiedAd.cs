using System;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.Entities
{
    /// <summary>
    /// </summary>
    public class ClassifiedAd : Entity
    {
        #region Initializers

        /// <summary>Initializes a new instance of the <see cref="ClassifiedAd" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <exception cref="ArgumentNullException">
        ///     id
        ///     or
        ///     ownerId
        /// </exception>
        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
            State = ClassifiedAdState.Inactive;

            EnsureValidState();
            Raise(new Events.ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>Gets the identifier.</summary>
        public ClassifiedAdId Id { get; }

        public UserId OwnerId { get; }
        public UserId ApprovedBy { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }
        public ClassifiedAdState State { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>Sets the title.</summary>
        /// <param name="title">The title.</param>
        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = Title
            });
        }

        /// <summary>Updates the text.</summary>
        /// <param name="text">The text.</param>
        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            EnsureValidState();

            Raise(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                Text = Text
            });
        }

        /// <summary>Updates the price.</summary>
        /// <param name="price">The price.</param>
        public void UpdatePrice(Price price)
        {
            Price = price;
            EnsureValidState();

            Raise(new Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                CurrencyCode = Price.Currency.CurrencyCode,
                Price = Price.Amount
            });
        }

        public void RequestToPublish()
        {
            State = ClassifiedAdState.PendingReview;
            EnsureValidState();

            Raise(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            });
        }

        protected void EnsureValidState()
        {
            var valid = Id is not null &&
                        OwnerId is not null &&
                        State switch
                        {
                            ClassifiedAdState.PendingReview =>
                                Title is not null &&
                                Text is not null &&
                                Price?.Amount > 0,
                            ClassifiedAdState.Active =>
                                Title is not null &&
                                Text is not null &&
                                Price?.Amount > 0 &&
                                ApprovedBy is not null,
                            _ => true
                        };

            if (!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}