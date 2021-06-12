using System;
using Marketplace.Domain.Services;
using Marketplace.Domain.ValueObjects;
using Marketplace.Tests.Fakes;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Linq;
using Marketplace.Domain.Exceptions;

namespace Marketplace.Tests.UnitTests
{
    [TestFixture]
    public class MoneyTests
    {
        private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

        [Test]
        [TestCase(5, "EUR")]
        [TestCase(5.23, "USD")]
        [TestCase(558, "JPY")]
        public void Money_ObjectsWithTheSameAmountEqualCurrency_AreEqual(decimal amount, string currencyCode)
        {
            var firstAmount = Money.FromDecimal(amount, currencyCode, CurrencyLookup);
            var secondAmount = Money.FromDecimal(amount, currencyCode, CurrencyLookup);

            Assert.AreEqual(firstAmount, secondAmount, "Money objects are not equal");
        }

        [Test]
        [TestCase(5.23, "EUR", "USD")]
        [TestCase(5, "USD", "JPY")]
        public void Money_ObjectsWithTheSameAmountDifferentCurrency_AreNotEqual(decimal amount, string currencyCode1, string currencyCode2)
        {
            var firstAmount = Money.FromDecimal(amount, currencyCode1, CurrencyLookup);
            var secondAmount = Money.FromDecimal(amount, currencyCode2, CurrencyLookup);

            Assert.AreNotEqual(firstAmount, secondAmount, "Money objects are equal");
        }

        [Test]
        [TestCase(5, "5.00", "EUR")]
        [TestCase(5.23, "5.23", "USD")]
        [TestCase(558, "558", "JPY")]
        public void Money_ObjectsFromDecimalAndFromString_AreEqual(decimal amountDecimal, string amountString, string currencyCode)
        {
            var firstAmount = Money.FromDecimal(amountDecimal, currencyCode, CurrencyLookup);
            var secondAmount = Money.FromString(amountString, currencyCode, CurrencyLookup);

            Assert.AreEqual(firstAmount, secondAmount, "Money objects are not equal");
        }

        [Test]
        [TestCase(10, "DEM")]
        public void Money_UnusedCurrencyNotAllowed_ThrowsArgumentException(decimal amount, string currencyCode)
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(amount, currencyCode, CurrencyLookup));
        }

        [Test]
        [TestCase(10, "ASDASDASAJSDH")]
        [TestCase(0, "123ads6ads")]
        public void Money_UnknownCurrencyNotAllowed_ThrowsArgumentException(decimal amount, string currencyCode)
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(amount, currencyCode, CurrencyLookup));
        }

        [Test]
        [TestCase(10.456, "EUR")]
        [TestCase(0.4658785, "JPY")]
        public void Money_TooManyDecimalPlaces_ThrowsArgumentOutOfRangeException(decimal amount, string currencyCode)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(amount, currencyCode, CurrencyLookup));
        }

        [Test]
        [TestCase(5, "EUR", 1, 2, 2)]
        [TestCase(50, "USD", 1, 2, 2, 5, 10, 10, 10, 5.5, 4.5)]
        public void Add_SumOfMoneyGivesFullAmount_AreEqual(decimal totalAmount, string currencyCode, params double[] partialAmounts)
        {
            var total = Money.FromDecimal(totalAmount, currencyCode, CurrencyLookup);
            var partials = partialAmounts.Select(t => Money.FromDecimal((decimal)t, currencyCode, CurrencyLookup)).ToList();

            var partialsTotal = Money.FromDecimal(0, currencyCode, CurrencyLookup);

            foreach (var partial in partials)
            {
                partialsTotal += partial;
            }

            Assert.AreEqual(total, partialsTotal);
        }

        [Test]
        [TestCase(5.23, "EUR", "USD")]
        [TestCase(5, "USD", "JPY")]
        public void Add_CannotAddObjectsWithDifferentCurrency_ThrowsCurrencyMismatchException(decimal amount, string currencyCode1, string currencyCode2)
        {
            var firstAmount = Money.FromDecimal(amount, currencyCode1, CurrencyLookup);
            var secondAmount = Money.FromDecimal(amount, currencyCode2, CurrencyLookup);

            Assert.Throws<CurrencyMismatchException>(() =>
            {
                var total = firstAmount + secondAmount; 
            });
        }

        [Test]
        [TestCase(5.23, "EUR", "USD")]
        [TestCase(5, "USD", "JPY")]
        public void Add_CannotSubtractObjectsWithDifferentCurrency_ThrowsCurrencyMismatchException(decimal amount, string currencyCode1, string currencyCode2)
        {
            var firstAmount = Money.FromDecimal(amount, currencyCode1, CurrencyLookup);
            var secondAmount = Money.FromDecimal(amount, currencyCode2, CurrencyLookup);

            Assert.Throws<CurrencyMismatchException>(() =>
            {
                var total = firstAmount - secondAmount;
            });
        }
    }
}