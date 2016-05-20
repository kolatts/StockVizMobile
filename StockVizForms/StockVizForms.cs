using System;

using Xamarin.Forms;
using System.Reactive.Linq;   // IMPORTANT - this makes await work!
using Akavache;

namespace StockVizForms
{
    public class App : Application
    {
        public App ()
        {


            // Make sure you set the application name before doing any inserts or gets
            BlobCache.ApplicationName = "StockVizForms";

            // Using async/await
            //var toaster = await BlobCache.UserAccount.GetObject<Toaster> ("toaster");
            // The root page of your application
            //var content = new ContentPage {
            //    Title = "StockVizForms",
            //    Content = new StackLayout {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
            //MainPage = new NavigationPage (content);
            var tabbedPage = new TabbedPage () {
                Title = "Stocks!"
            };

            tabbedPage.Children.Add (new SearchStockPage ());
            tabbedPage.Children.Add (new FavoriteStocksPage ());
            MainPage = new NavigationPage (tabbedPage);
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

