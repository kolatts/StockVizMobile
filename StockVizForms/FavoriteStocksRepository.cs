using Akavache;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using StockVizForms.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace StockVizForms
{
    public interface IFavoriteStocksRepository
    {
        Task StoreStock(Stock stock);

        Task<List<Stock>> GetFavoriteStocks();

        Task DeleteFavoriteStock(string symbol);

        Task<Stock> GetFavoriteStock(string symbol);
    }

    public class AzureFavoriteStocksRepository : IFavoriteStocksRepository
    {
        private MobileServiceClient client;
#if OFFLINE_SYNC_ENABLED
#else
        private IMobileServiceTable<Stock> stockTable;
#endif

        private AzureFavoriteStocksRepository()
        {
            this.client = new MobileServiceClient(MobileConfig.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<Stock>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.todoTable = client.GetSyncTable<Stock>();
#else
            this.stockTable = client.GetTable<Stock>();
#endif
        }

        public static AzureFavoriteStocksRepository DefaultRepository { get; private set; } = new AzureFavoriteStocksRepository();
        public MobileServiceClient CurrentClient => client;

        public bool IsOfflineEnabled => stockTable is IMobileServiceSyncTable<Stock>;

        public async Task StoreStock(Stock stock)
        {
            if (stock.Id == null)
            {
                await stockTable.InsertAsync(stock);
            }
            else
            {
                await stockTable.UpdateAsync(stock);
            }
        }

        public async Task<List<Stock>> GetFavoriteStocks()
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<Stock> items = await stockTable
                    .ToEnumerableAsync();

                return new List<Stock>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task DeleteFavoriteStock(string symbol)
        {
            var existingList = await stockTable.Where(x => x.Symbol == symbol).Take(1).ToListAsync();
            if (existingList.Count == 0) return;
            await stockTable.DeleteAsync(existingList.First());
        }

        public async Task<Stock> GetFavoriteStock(string symbol)
        {
            var existingList = await stockTable.Where(x => x.Symbol == symbol).Take(1).ToListAsync();
            if (existingList.Count == 0) return null;
            return existingList.First();
        }
    }

    public class AvakacheFavoriteStocksRepository : IFavoriteStocksRepository
    {
        public async Task StoreStock(Stock stock)
        {
            await BlobCache.LocalMachine.InsertObject(stock.Symbol, stock);
        }

        public async Task<List<Stock>> GetFavoriteStocks()
        {
            return (List<Stock>)(await BlobCache.LocalMachine.GetAllObjects<Stock>());
        }

        public async Task DeleteFavoriteStock(string symbol)
        {
            await BlobCache.LocalMachine.Invalidate(symbol);
        }

        public async Task<Stock> GetFavoriteStock(string symbol)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<Stock>(symbol);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}