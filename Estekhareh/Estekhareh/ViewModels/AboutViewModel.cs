using Estekhareh.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        public ICommand OpenEstekhareResultCommand { get; }

        public AboutViewModel()
        {
            Title = "استخاره";
            OpenEstekhareResultCommand = new Command(async() => await GoToResultPage());
        }

        private async Task GoToResultPage()
        {
            // await Browser.OpenAsync(""));
            var aya = GetRandomStartAya().ToString();
            await Shell.Current.GoToAsync($"//{ nameof(ItemsPage)}?{nameof(ItemsViewModel.StartAya)}={aya}", true);
        }

        private int GetRandomStartAya()
        {
            Random random = new Random(Environment.TickCount);
            return random.Next(2, 6236);
        }
    }
}