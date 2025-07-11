using Margo.HedgePricer.Core.Model;
using System.Threading.Channels;

namespace Margo.HedgePricer.Core.Producer
{
    public class QuoteEmitter
    {
        private readonly ChannelWriter<Quote> _writer;
        private readonly RandomQuoteFactory _quoteFactory;
        private volatile bool _running = true;

        public QuoteEmitter(ChannelWriter<Quote> writer, RandomQuoteFactory quoteFactory)
        {
            _writer = writer;
            _quoteFactory = quoteFactory;
        }

        public async Task Run()
        {
            while (_running)
            {
                var quote = _quoteFactory.GenerateQuote();
                await _writer.WriteAsync(quote);

                await Task.Delay(1000);  // Délai entre les quotes
            }
        }

        public void Stop() => _running = false;
    }
} 