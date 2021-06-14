using System;

namespace Marketplace.Domain.Events
{
    public class ClassifiedAdCreated
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}