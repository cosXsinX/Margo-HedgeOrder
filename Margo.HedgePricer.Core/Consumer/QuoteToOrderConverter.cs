using Margo.HedgePricer.Core.Model;

namespace Margo.HedgePricer.Core.Consumer
{
    public interface IQuoteToOrderConverter
    {
        Order Convert(Quote quote);
    }

    public class QuoteToOrderConverter : IQuoteToOrderConverter
    {
        private readonly Random _random = new Random();

        public Order Convert(Quote quote)
        {
            //créé un order d'hedge de position contraire à la patte bid ou ask de la quote, de même quantité selon que le bid est plus petit ou plus grand que le ask
            Side orderSide = quote.Bid> quote.Ask ? Side.Ask : Side.Bid;

            return orderSide switch
            {
                Side.Bid => new Order
                {
                    Price = quote.Ask,
                    Quantity = quote.Quantity,
                    Side = Side.Bid,
                    Instrument = quote.Instrument
                },
                Side.Ask => new Order
                {
                    Price = quote.Bid,
                    Quantity = quote.Quantity,
                    Side = Side.Ask,
                    Instrument = quote.Instrument
                },
                _ => throw new ArgumentException($"Unsupported order side: {orderSide}")
            };
        }
    }
} 