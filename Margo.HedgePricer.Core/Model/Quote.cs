namespace Margo.HedgePricer.Core.Model
{
    public class Quote
    {
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public int Quantity { get; set; }
        public string Instrument { get; set; }  // Taille fixe 12

        public Quote(decimal bid, decimal ask, int quantity, string instrument)
        {
            Bid = bid;
            Ask = ask;
            Quantity = quantity;
            Instrument = instrument.PadRight(12).Substring(0, 12);  // Force taille 12
        }
    }
}
