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
            this.SearchResults.ItemSelected += OnSelection;
        }

        public async void OnSelection (object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null) {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            var stock = (Stock)e.SelectedItem; //We have to cast this to its type

            await Navigation.PushAsync (new StockDetailPage (stock));
            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

    }
}

