using System;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework.Helpers;

namespace Marketplace.Domain.Entities
{
    public class Picture : Entity<PictureId>
    {
        internal PictureSize Size { get; set; }
        internal Uri Location { get; set; }
        internal int Order { get; set; }

        public Picture(Action<object> applier) : base(applier)
        {
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PictureAddedToClassifiedAd e:
                    Id = new PictureId(e.PictureId);
                    Location = new Uri(e.Url);
                    Size = new PictureSize
                    {
                        Width = e.Width,
                        Height = e.Height,
                    };
                    Order = e.Order;
                    break;

                case Events.ClassifiedAdPictureResized e:
                    Size = new PictureSize
                    {
                        Width = e.Width,
                        Height = e.Height,
                    };
                    break;
            }
        }

        public void Resize(PictureSize newSize)
        {
            Apply(new Events.ClassifiedAdPictureResized
            {
                PictureId = Id,
                Width = newSize.Width,
                Height = newSize.Height,
            });
        }
    }
}