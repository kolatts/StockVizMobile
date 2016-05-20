using System;
using System.Collections.Generic;
using StockVizForms.Model;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace StockVizForms
{
    public partial class StockDetailPage : ContentPage
    {
        private StockManager _manager;
        private List<StockDayQuote> history;
        private bool favoriteStock;
        private Stock Stock { get; set; }
        public StockDetailPage (Stock stock)
        {
            Stock = stock;
            _manager = new StockManager ();
            InitializeComponent ();

            //This isn't the best way to put data in the view.


            StockName.Text = Stock.Name;
            StockSymbol.Text = $"({Stock.Symbol})";
            StockExchangeDisplay.Text = Stock.ExchDisp;
            StockTypeDisplay.Text = Stock.TypeDisp;
            LoadFavoriteStatus ();
            LoadHistory ();

        }
        protected async void LoadFavoriteStatus ()
        {
            favoriteStock = await _manager.GetFavoriteStock (Stock.Symbol) != null;
            AddRemoveStock.Command = new Command (OnAddRemoveStock);
            AddRemoveStock.Text = favoriteStock ? "Unwatch" : "Watch";
        }

        protected async void LoadHistory ()
        {
            history  = await _manager.GetHistory (Stock.Symbol);
            this.HistoryList.ItemsSource =  history;
        }

        protected async void OnAddRemoveStock ()
        {
            if (favoriteStock)
                await _manager.DeleteFavoriteStock (Stock.Symbol);
            else
                await _manager.StoreStock (Stock);
            favoriteStock = !favoriteStock;
            AddRemoveStock.Text = favoriteStock ? "Unwatch" : "Watch";
        }
    }
}

