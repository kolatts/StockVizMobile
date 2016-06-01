using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockVizForms.Entities
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