using Estekhareh.ViewModels;
using Estekhareh.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Estekhareh
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }
 
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SettingPage)}");
        }
    }
}
