using Estekhareh.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "استخاره";
            OpenEstekhareResultCommand = new Command(async() => {
                // await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
               
                await Shell.Current.GoToAsync($"//{ nameof(ItemsPage)}", true);
            });
        }

        public ICommand OpenEstekhareResultCommand { get; }
    }
}