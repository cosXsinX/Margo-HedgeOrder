namespace Margo.HedgePricer.Core.Model
{
    public class Order
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Side Side { get; set; }
        public string Instrument { get; set; }

        public override string ToString()
        {
            return $"Order: {Side} {Quantity} @ {Price} on {Instrument}";
        }
    }
}
