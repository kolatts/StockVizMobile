using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
namespace StockVizForms
{
    public partial class SearchStockPage : ContentPage
    {
        public SearchStockPage ()
        {
            InitializeComponent ();

            this.StockSearchBar.SearchCommand 
                = new Command ( () => {

            });
        }


    }
}

