using Estekhareh.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }

        public SettingViewModel()
        {
            SaveCommand = new Command(OnSaveClicked);
        }

        private async void OnSaveClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
