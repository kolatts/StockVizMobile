using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using StockVizForms.Model;

namespace StockVizForms
{
    public partial class SearchStockPage : ContentPage
    {
        private StockManager _manager;
        private List<Stock> stocks;

        public SearchStockPage ()
        {
            InitializeComponent ();
            _manager = new StockManager ();

            this.StockSearchBar.SearchCommand 
                = new Command ( () => {
                    stocks = _manager.GetStocks (StockSearchBar.Text);
            });
        }


    }
}

