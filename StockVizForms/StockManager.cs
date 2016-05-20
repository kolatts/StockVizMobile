using System;
using StockVizForms.Model;
using System.Net.Http;
using ModernHttpClient;
using System.Collections.Generic;

namespace StockVizForms
{
    public class StockManager
    {
        public StockManager ()
        {
        }

        public List<Stock> GetStocks (string symbol)
        {
            List<Stock> result = null;
            var url = $"http://d.yimg.com/autoc.finance.yahoo.com/autoc?query={symbol}&" +
                  "region=1&lang=en";

            using (var client = GetClient(url))
            {
                 var responseString = client.GetStringAsync(string.Empty).Result;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<YahooStockLookupResult> (responseString);
                result = response.ResultSet.Result;
            }
            return result;
        }
        protected HttpClient GetClient (string url)
        {
            return new HttpClient (new NativeMessageHandler ()) {
                BaseAddress = new Uri (url),
            };
        }
    }
}

