using Margo.HedgePricer.Core.Model;
using System.Threading.Channels;

namespace Margo.HedgePricer.Core.Consumer
{
    public class OrderConsumer
    {
        private readonly ChannelReader<Quote> _reader;
        private readonly IQuoteToOrderConverter _converter;
        private volatile bool _running = true;

        public event Action<Order>? OrderCreated;

        public OrderConsumer(ChannelReader<Quote> reader, IQuoteToOrderConverter converter)
        {
            _reader = reader;
            _converter = converter;
        }

        public async Task Run()
        {
            await foreach (var quote in _reader.ReadAllAsync())
            {
                if (!_running) break;
                var order = _converter.Convert(quote);
                OrderCreated?.Invoke(order);
            }
        }

        public void Stop() => _running = false;
    }
}
