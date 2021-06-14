using System;

namespace Marketplace.Domain.Events
{
    public class ClassifiedAdTextUpdated
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}