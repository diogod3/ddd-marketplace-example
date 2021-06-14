using System;

namespace Marketplace.Domain.Events
{
    public class ClassifiedAdPriceUpdated
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
    }
}