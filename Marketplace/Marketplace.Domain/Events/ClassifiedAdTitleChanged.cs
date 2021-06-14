using System;

namespace Marketplace.Domain.Events
{
    public class ClassifiedAdTitleChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}