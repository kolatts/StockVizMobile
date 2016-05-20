using System;
using StockVizForms.Model;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Akavache;
using System.Reactive.Linq;
namespace StockVizForms
{
    public class StockManager
    {
        private IFavoriteStocksRepository _repo;
        public StockManager ()
        {
            _repo = new AvakacheFavoriteStocksRepository ();
        }

        public async Task<List<Stock>> GetStocks (string symbol)
        {
            List<Stock> result = null;
            var url = $"http://d.yimg.com/autoc.finance.yahoo.com/autoc?query={symbol}&" +
                  "region=1&lang=en";

            using (var client = GetClient(url))
            {
                var responseString = await client.GetStringAsync(string.Empty);
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<YahooStockLookupResult> (responseString);
                result =  response.ResultSet.Result.Where(x=>x.TypeDisp == "Equity" && "NYSE,NASDAQ".Contains(x.ExchDisp)).ToList()  ;
            }
            return result;
        }

        public async Task<List<StockDayQuote>> GetHistory (string symbol, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<StockDayQuote> result = new List<StockDayQuote> ();
            var start = startDate ?? DateTime.Today.AddDays (-14);
            var end = endDate ?? DateTime.Today;
            var url = "http://ichart.finance.yahoo.com/table.csv?" +
                $"s={symbol.Trim ()}&" +
                $"a={start.Month - 1:00}&b={start.Day:00}&c={start.Year}&" +
                $"d={end.Month - 1:00}&e={end.Day:00}&f={end.Year}&" +
                "g=d&ignore=.csv";

            using (var client = GetClient (url)) 
            {
                var responseString = await client.GetStringAsync (string.Empty);
                result = DeserializeCsv (responseString, symbol);
            }
            return result;
        }



        protected List<StockDayQuote> DeserializeCsv (string responseString, string symbol)
        {
            return responseString
                .Split (new string[]{ "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Skip (1)
                .Select (x => 
                {
                    var fields = x.Replace ("\r", string.Empty).Split (',');
                    return new StockDayQuote () {
                        Date = DateTime.Parse (fields [0]),
                        Open = float.Parse(fields[1]),
                        High = float.Parse(fields[2]),
                        Low = float.Parse(fields[3]),
                        Close = float.Parse(fields[4]),
                        Volume = long.Parse(fields[5]),
                        AdjustedClose = float.Parse(fields[6]),
                        Symbol = symbol

                    };
                })
                .ToList();
        }


        protected HttpClient GetClient (string url)
        {
            return new HttpClient (new NativeMessageHandler ()) {
                BaseAddress = new Uri (url),
            };
        }

        public Task StoreStock (Stock stock)
        {
            return _repo.StoreStock (stock);
        }

        public Task<List<Stock>> GetFavoriteStocks ()
        {
            return _repo.GetFavoriteStocks ();
        }

        public Task DeleteFavoriteStock (string symbol)
        {
            return _repo.DeleteFavoriteStock (symbol);
        }

        public Task<Stock> GetFavoriteStock (string symbol)
        {
            return _repo.GetFavoriteStock (symbol);
        }
    }
}

