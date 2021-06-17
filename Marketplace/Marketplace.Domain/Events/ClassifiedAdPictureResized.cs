using System;

namespace Marketplace.Domain.Events
{
    public class ClassifiedAdPictureResized
    {
        public Guid PictureId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}