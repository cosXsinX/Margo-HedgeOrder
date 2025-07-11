using Margo.HedgePricer.Core.Consumer;
using Margo.HedgePricer.Core.Model;
using NUnit.Framework;
using System;

namespace Margo.HedgePrice.Tests
{
    [TestFixture]
    public class QuoteToOrderConverterTests
    {
        private QuoteToOrderConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new QuoteToOrderConverter();
        }

        [Test]
        public void Convert_BidGreaterThanAsk_ReturnsOrderWithSideAskAndPriceBid()
        {
            var quote = new Quote(105m, 100m, 10, "EURUSD");
            var order = _converter.Convert(quote);
            Assert.AreEqual(Side.Ask, order.Side);
            Assert.AreEqual(105m, order.Price);
            Assert.AreEqual(10, order.Quantity);
            Assert.AreEqual("EURUSD".PadRight(12), order.Instrument);
        }

        [Test]
        public void Convert_BidLessThanAsk_ReturnsOrderWithSideBidAndPriceAsk()
        {
            var quote = new Quote(95m, 100m, 5, "USDJPY");
            var order = _converter.Convert(quote);
            Assert.AreEqual(Side.Bid, order.Side);
            Assert.AreEqual(100m, order.Price);
            Assert.AreEqual(5, order.Quantity);
            Assert.AreEqual("USDJPY".PadRight(12), order.Instrument);
        }

        [Test]
        public void Convert_BidEqualsAsk_ReturnsOrderWithSideBidAndPriceAsk()
        {
            var quote = new Quote(100m, 100m, 7, "GBPCHF");
            var order = _converter.Convert(quote);
            Assert.AreEqual(Side.Bid, order.Side);
            Assert.AreEqual(100m, order.Price);
            Assert.AreEqual(7, order.Quantity);
            Assert.AreEqual("GBPCHF".PadRight(12), order.Instrument);
        }
    }
} 