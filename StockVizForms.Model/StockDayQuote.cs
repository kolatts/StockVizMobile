using System;
namespace StockVizForms.Model
{
    public class StockDayQuote
    {
        public DateTime Date { get; set; }
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get;set;}
        public long Volume { get; set; }
        public float AdjustedClose { get; set; }
        public string Symbol { get; set; }

        public StockDayQuote ()
        {
        }

    }
}

