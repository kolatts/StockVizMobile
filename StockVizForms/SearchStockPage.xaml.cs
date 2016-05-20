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

        public SearchStockPage ()
        {
            InitializeComponent ();
            _manager = new StockManager ();

            this.StockSearchBar.SearchCommand 
                = new Command ( async () => {
                SearchResults.ItemsSource = await _manager.GetStocks (StockSearchBar.Text);
            });
        }


    }
}

