using System;

namespace YahooFinanceApi
{
    public sealed class Candle: ITick
    {
        public DateTime DateTime { get;  set; }

        public decimal Open { get;  set; }

        public decimal High { get;  set; }

        public decimal Low { get;  set; }

        public decimal Close { get;  set; }

        public long Volume { get;  set; }

        public decimal AdjustedClose { get;  set; }
    }
}
