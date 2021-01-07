using Estekhareh.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Estekhareh.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            GoToMain();
            return true;
        }

        private async void GoToMain()
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}