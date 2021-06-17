﻿using Marketplace.Domain.Events;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Framework.Aggregates;

namespace Marketplace.Domain.Entities
{
    /// <summary></summary>
    /// <seealso cref="Marketplace.Framework.Helpers.Entity"/>
    public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
    {
        #region Properties

        ///// <summary>Gets the identifier.</summary>
        //public ClassifiedAdId Id { get; private set; }

        /// <summary>Gets the owner identifier.</summary>
        public UserId OwnerId { get; private set; }

        /// <summary>Gets the approved by.</summary>
        public UserId ApprovedBy { get; private set; }

        /// <summary>Gets the title.</summary>
        public ClassifiedAdTitle Title { get; private set; }

        /// <summary>Gets the text.</summary>
        public ClassifiedAdText Text { get; private set; }

        /// <summary>Gets the price.</summary>
        public Price Price { get; private set; }

        /// <summary>Gets the state.</summary>
        public ClassifiedAdState State { get; private set; }

        public List<Picture> Pictures { get; } = new List<Picture>();

        private Picture FirstPicture => Pictures.OrderBy(t => t.Order).FirstOrDefault();

        #endregion

        #region Initializers

        /// <summary>Initializes a new instance of the <see cref="ClassifiedAd"/> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <exception cref="ArgumentNullException"><param name="id"/> or <param name="ownerId"/></exception>
        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (ownerId is null)
            {
                throw new ArgumentNullException(nameof(ownerId));
            }

            Apply(new ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        #endregion

        #region Public Methods

        /// <summary>Whens the specified event.</summary>
        /// <param name="event">The event.</param>
        /// <exception cref="EventNotSupportedException"></exception>
        protected override void When(object @event)
        {
            switch (@event)
            {
                case ClassifiedAdCreated e:
                    Id = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    break;

                case ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;

                case ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.Text);
                    break;

                case ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrencyCode);
                    break;

                case ClassifiedAdSentForReview:
                    State = ClassifiedAdState.PendingReview;
                    break;

                case PictureAddedToClassifiedAd e:
                    var picture = new Picture(Apply);
                    ApplyToEntity(picture, e);
                    Pictures.Add(picture);
                    break;

                default:
                    throw new EventNotSupportedException(@event);
            }
        }

        /// <summary>Ensures the valid state of the entity.</summary>
        /// <exception cref="InvalidEntityStateException">Post-checks failed in state {State}</exception>
        protected override void EnsureValidState()
        {
            var valid = Id is not null &&
                        OwnerId is not null &&
                        State switch
                        {
                            ClassifiedAdState.PendingReview =>
                                Title is not null &&
                                Text is not null &&
                                Price?.Amount > 0 &&
                                FirstPicture.HasCorrectSize(),
                            ClassifiedAdState.Active =>
                                Title is not null &&
                                Text is not null &&
                                Price?.Amount > 0 &&
                                ApprovedBy is not null &&
                                FirstPicture.HasCorrectSize(),
                            _ => true
                        };

            if (!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
            }
        }

        /// <summary>Sets the title.</summary>
        /// <param name="title">The title.</param>
        public void SetTitle(ClassifiedAdTitle title)
        {
            Apply(new ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            });
        }

        /// <summary>Updates the text.</summary>
        /// <param name="text">The text.</param>
        public void UpdateText(ClassifiedAdText text)
        {
            Apply(new ClassifiedAdTextUpdated
            {
                Id = Id,
                Text = text
            });
        }

        /// <summary>Updates the price.</summary>
        /// <param name="price">The price.</param>
        public void UpdatePrice(Price price)
        {
            Apply(new ClassifiedAdPriceUpdated
            {
                Id = Id,
                CurrencyCode = price.Currency.CurrencyCode,
                Price = price.Amount
            });
        }

        /// <summary>Requests to publish.</summary>
        public void RequestToPublish()
        {
            Apply(new ClassifiedAdSentForReview
            {
                Id = Id
            });
        }

        public void AddPicture(Uri pictureUri, PictureSize size)
        {
            Apply(new PictureAddedToClassifiedAd
            {
                ClassifiedAdId = Id,
                PictureId = Guid.NewGuid(),
                Url = pictureUri.ToString(),
                Width = size.Width,
                Height = size.Height,
                Order = Pictures.Max(t => t.Order) + 1
            });
        }

        public void ResizePicture(PictureId pictureId, PictureSize newSize)
        {
            var picture = FindPicture(pictureId);

            if (picture is null)
            {
                throw new InvalidOperationException("Cannot resize a picture that I don't have");
            }

            picture.Resize(newSize);
        }

        #endregion

        #region Private Methods

        private Picture FindPicture(PictureId id)
        {
            return Pictures.FirstOrDefault(t => t.Id == id);
        }



        #endregion
    }
}