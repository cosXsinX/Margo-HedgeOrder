using Margo.HedgePricer.Core.Model;
using NUnit.Framework;
using System;

namespace Margo.HedgePrice.Tests
{
    [TestFixture]
    public class RandomQuoteFactoryTests
    {
        // Helper subclass to control randomness
        private class TestableRandomQuoteFactory : Margo.HedgePricer.Core.Producer.RandomQuoteFactory
        {
            private readonly double _nextDouble;
            private readonly int _nextForAsk;
            private readonly int _nextForQuantity;
            public TestableRandomQuoteFactory(string instrument, double nextDouble, int nextForAsk, int nextForQuantity)
                : base(instrument)
            {
                _nextDouble = nextDouble;
                _nextForAsk = nextForAsk;
                _nextForQuantity = nextForQuantity;
            }
            private int _callCount = 0;
            protected override Random Rnd => new TestRandom(_nextDouble, _nextForAsk, _nextForQuantity, ref _callCount);
            private class TestRandom : Random
            {
                private readonly double _nextDouble;
                private readonly int _nextForAsk;
                private readonly int _nextForQuantity;
                private int _callCount;
                public TestRandom(double nextDouble, int nextForAsk, int nextForQuantity, ref int callCount)
                {
                    _nextDouble = nextDouble;
                    _nextForAsk = nextForAsk;
                    _nextForQuantity = nextForQuantity;
                    _callCount = 0;
                }
                public override double NextDouble() { _callCount++; return _nextDouble; }
                public override int Next(int minValue, int maxValue)
                {
                    // 1st call: for ask (Next(2)), 2nd: for quantity (Next(1, 100))
                    if (_callCount == 1) { _callCount++; return _nextForAsk; }
                    if (_callCount == 2) { _callCount++; return _nextForQuantity; }
                    return base.Next(minValue, maxValue);
                }
            }
        }

        [Test]
        public void GenerateQuote_InstrumentIsAlways12Chars()
        {
            var factory = new TestableRandomQuoteFactory("X", 0.05, 1, 1);
            var quote = factory.GenerateQuote();
            Assert.AreEqual(12, quote.Instrument.Length);
        }

        [Test]
        public void GenerateQuote_QuantityWithinRange()
        {
            var factory = new TestableRandomQuoteFactory("EURUSD", 0.05, 1, 99);
            var quote = factory.GenerateQuote();
            Assert.GreaterOrEqual(quote.Quantity, 1);
            Assert.LessOrEqual(quote.Quantity, 99);
        }
    }
} 