using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akavache;
using StockVizForms.Model;
using System.Reactive.Linq;
namespace StockVizForms
{
    
    public interface IFavoriteStocksRepository
    {
        Task StoreStock (Stock stock);
         Task<List<Stock>> GetFavoriteStocks ();
         Task DeleteFavoriteStock (string symbol);
         Task<Stock> GetFavoriteStock (string symbol);
    }

    public class AvakacheFavoriteStocksRepository : IFavoriteStocksRepository
    {
        public async Task StoreStock (Stock stock)
        {
            await BlobCache.LocalMachine.InsertObject (stock.Symbol, stock);
        }

        public async Task<List<Stock>> GetFavoriteStocks ()
        {
            return (List<Stock>)(await BlobCache.LocalMachine.GetAllObjects<Stock> ());
        }

        public async Task DeleteFavoriteStock (string symbol)
        {
            await BlobCache.LocalMachine.Invalidate (symbol);
        }

        public async Task<Stock> GetFavoriteStock (string symbol)
        {
            try {
                return await BlobCache.LocalMachine.GetObject<Stock> (symbol);
            } catch (Exception ex) {
                return null;
            }

        }
    }
}

