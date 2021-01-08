using Estekhareh.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public ICommand OpenEstekhareResultCommand { get; }

        public MainViewModel()
        {
            Title = "استخاره";
            OpenEstekhareResultCommand = new Command(async() => await GoToResultPage());
        }

        private async Task GoToResultPage()
        {
            // await Browser.OpenAsync(""));
            var index = GetRandomStartIndex().ToString();
            await Shell.Current.GoToAsync($"//{ nameof(ItemsPage)}?{nameof(ItemsViewModel.StartIndex)}={index}", true);
        }

        private int GetRandomStartIndex()
        {
            Random random = new Random(Environment.TickCount);
            return random.Next(2, 6236);
        }
    }
}