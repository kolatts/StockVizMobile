using System;
using System.Collections.Generic;

namespace StockVizForms.Model
{
    /// <summary>
    /// Yahoo stock DTO.
    /// </summary>
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exch { get; set; }
        public string Type { get; set; }
        public string ExchDisp { get; set; }
        public string TypeDisp { get; set; }

        public Stock ()
        {
        }
    }

    public class YahooStockLookupResult
    {
        public YahooStockLookupResultSet ResultSet { get; set; }
    }

    public class YahooStockLookupResultSet
    {
        public string Query { get; set; }
        public List<Stock> Result { get; set; }
    }
}

