using Margo.HedgePricer.Core.Model;

namespace Margo.HedgePricer.Core.Producer
{
    public class RandomQuoteFactory
    {
        private readonly string _instrument;
        private readonly Random _rnd = new Random();

        public decimal BasePrice { get; set; } = 100.00m;
        public decimal Spread { get; set; } = 0.05m;

        protected virtual Random Rnd => _rnd;

        public RandomQuoteFactory(string instrument)
        {
            _instrument = instrument;
        }

        public Quote GenerateQuote()
        {
            // Add some random variation to the base price (±5%)
            decimal priceVariation = (decimal)(Rnd.NextDouble() * 0.1 - 0.05); // ±5%
            decimal currentBasePrice = BasePrice * (1 + priceVariation);
            currentBasePrice = Math.Round(currentBasePrice, 2);

            decimal bid = currentBasePrice;
            decimal ask = currentBasePrice + Spread * (Rnd.Next(2) == 1?1:-1);
            int quantity = Rnd.Next(1, 100);

            return new Quote(bid, ask, quantity, _instrument);
        }
    }
} 