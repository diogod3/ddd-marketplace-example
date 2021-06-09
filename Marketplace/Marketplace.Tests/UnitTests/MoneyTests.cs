using Marketplace.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Tests.UnitTests
{
    [TestFixture]
    public class MoneyTests
    {
        [Test]
        public void Money_ObjectsWithTheSameAmount_AreEqual()
        {
            var firstAmount = new Money(5);
            var secondAmount = new Money(5);

            Assert.AreEqual(firstAmount, secondAmount, "Money objects are not equal");
            Assert.IsTrue(firstAmount == secondAmount, "Money objects are not equal");
        }

        [Test]
        public void Add_SumOfMoneyGivesFullAmount_AreEqual()
        {
            var coin1 = new Money(1);
            var coin2 = new Money(2);
            var coin3 = new Money(2);
            var bankNote = new Money(5);

            Assert.AreEqual(bankNote, coin1 + coin2 + coin3);
        }
    }
}
