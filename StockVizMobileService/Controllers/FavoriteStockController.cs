using Microsoft.Azure.Mobile.Server;
using StockVizMobileService.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Stock = StockVizMobileService.DataObjects.Stock;

namespace StockVizMobileService.Controllers
{
    public class FavoriteStockController : TableController<Stock>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            StockVizMobileContext context = new StockVizMobileContext();
            DomainManager = new EntityDomainManager<Stock>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Stock> GetAllStocks()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Stock> GetStock(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Stock> PatchStock(string id, Delta<Stock> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostStock(Stock item)
        {
            Stock current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteStock(string id)
        {
            return DeleteAsync(id);
        }
    }
}