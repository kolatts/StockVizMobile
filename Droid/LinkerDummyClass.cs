using System;
using Splat;
using StockVizForms.Model;

namespace StockVizForms.Droid
{
    public static class ShutUpLinkerIDoWhatIWant
    {
        public static string StopDeletingMyStuff ()
        {
            return typeof (WrappingFullLogger).ToString ();
        }

        public static Type Blah = typeof (Akavache.BlobCache);
    }
}

