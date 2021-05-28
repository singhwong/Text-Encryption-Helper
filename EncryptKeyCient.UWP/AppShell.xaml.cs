using EncryptKeyCient.UWP.ViewModels;
using EncryptKeyCient.UWP.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EncryptKeyCient.UWP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
