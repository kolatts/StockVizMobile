using Microsoft.WindowsAzure.Mobile.Service;

namespace StockVizForms.Azure.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}