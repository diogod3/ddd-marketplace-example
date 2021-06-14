using System;
using Marketplace.Domain;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Services;
using Marketplace.Domain.ValueObjects;
using Marketplace.Tests.Fakes;
using NUnit.Framework;

namespace Marketplace.Tests.UnitTests
{
    [TestFixture]
    public class ClassifiedAdTests
    {
        private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

        private ClassifiedAd _classifiedAd;

        [SetUp]
        public void SetUp()
        {
            _classifiedAd = new ClassifiedAd(new ClassifiedAdId(Guid.NewGuid()),
                                          new UserId(Guid.NewGuid()));
        }

        #region RequestToPublish

        [Test]
        [TestCase("Test ad", "Test ad description", 10, "EUR")]
        public void RequestToPublish_ValidAd_AreEqual(string title, string text, decimal price, string currencyCode)
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString(title));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString(text));
            _classifiedAd.UpdatePrice(Price.FromDecimal(price, currencyCode, CurrencyLookup));
            _classifiedAd.RequestToPublish();

            Assert.AreEqual(ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Test]
        [TestCase("Test ad description", 10, "EUR")]
        public void RequestToPublish_InvalidAdNoTitle_ThrowsInvalidEntityStateException(string text, decimal price, string currencyCode)
        {
            _classifiedAd.UpdateText(ClassifiedAdText.FromString(text));
            _classifiedAd.UpdatePrice(Price.FromDecimal(price, currencyCode, CurrencyLookup));
            
            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Test]
        [TestCase("Test ad", 10, "EUR")]
        public void RequestToPublish_InvalidAdNoText_ThrowsInvalidEntityStateException(string title, decimal price, string currencyCode)
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString(title));
            _classifiedAd.UpdatePrice(Price.FromDecimal(price, currencyCode, CurrencyLookup));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Test]
        [TestCase("Test ad", "Test ad description")]
        public void RequestToPublish_InvalidAdNoPrice_ThrowsInvalidEntityStateException(string title, string text)
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString(title));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString(text));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Test]
        [TestCase("Test ad", "Test ad description", 0, "EUR")]
        public void RequestToPublish_InvalidAdZeroPrice_ThrowsInvalidEntityStateException(string title, string text, decimal price, string currencyCode)
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString(title));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString(text));
            _classifiedAd.UpdatePrice(Price.FromDecimal(price, currencyCode, CurrencyLookup));

            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        #endregion
    }
}