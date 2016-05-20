using System;
using System.Collections.Generic;
using StockVizForms.Model;
using Xamarin.Forms;

namespace StockVizForms
{
    public partial class FavoriteStocksPage : ContentPage
    {
        private StockManager _manager;
        private List<Stock> stocks;
        public FavoriteStocksPage ()
        {
            _manager = new StockManager ();
            InitializeComponent ();
            LoadFavoriteStocks ();
            StockList.ItemSelected += OnSelection;
        }
        protected async void LoadFavoriteStocks ()
        {
            stocks = await _manager.GetFavoriteStocks ();
            StockList.ItemsSource = stocks;
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

        protected override void OnAppearing ()
        {
            LoadFavoriteStocks ();
            base.OnAppearing ();
        }
    }
}

