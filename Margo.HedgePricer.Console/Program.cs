// See https://aka.ms/new-console-template for more information
using Margo.HedgePricer.Core.Consumer;
using Margo.HedgePricer.Core.Model;
using Margo.HedgePricer.Core.Producer;
using System.Threading.Channels;

var channel = Channel.CreateUnbounded<Quote>(new UnboundedChannelOptions
{
    SingleReader = false,
    SingleWriter = false
});

var quoteFactory = new RandomQuoteFactory("EUR/USD")
{
    BasePrice = 1.0850m,  // EUR/USD mid price
    Spread = 0.0002m       // 2 pips spread
};
var quoteEmitter = new QuoteEmitter(channel.Writer, quoteFactory);
var quoteToOrderConverter = new QuoteToOrderConverter();
var orderManager = new OrderConsumer(channel.Reader, quoteToOrderConverter);

// Subscribe to the OrderCreated event
orderManager.OrderCreated += (order) => Console.WriteLine(order);

var emitterTask = Task.Run(async () => await quoteEmitter.Run());
var managerTask = Task.Run(async () => await orderManager.Run());

Thread.Sleep(10000); // Laisse tourner 10 secondes

quoteEmitter.Stop();
orderManager.Stop();
channel.Writer.Complete(); // Signale la fin au consommateur

Task.WaitAll(emitterTask, managerTask);
