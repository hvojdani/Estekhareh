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
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
        }
 
        private async void OnSettingMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync($"{nameof(SettingPage)}");
        }

        private async void OnLastResultMenuClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync($"{nameof(ItemsPage)}");
        }
    }
}
