using Microsoft.Azure.Mobile.Server;

namespace StockVizMobileService.DataObjects
{
    public class Stock : EntityData
    {
        public int StockId { get; set; }

        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exch { get; set; }
        public string Type { get; set; }
        public string ExchDisp { get; set; }
        public string TypeDisp { get; set; }
    }
}