using Estekhareh.Models;
using Estekhareh.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }

        private bool _isTranslateEnabled;
        public bool IsTranslateEnabled
        {
            get => _isTranslateEnabled;
            set => SetProperty(ref _isTranslateEnabled, value);
        }

        private TranslatorModel _selectedTranslator;
        public TranslatorModel SelectedTranslator
        {
            get => _selectedTranslator;
            set => SetProperty(ref _selectedTranslator, value);
        }

        public SettingViewModel()
        {
            Translators = new ObservableCollection<TranslatorModel>();
            SaveCommand = new Command(OnSaveClicked);
        }

        public ObservableCollection<TranslatorModel> Translators { get; }

        private async void OnSaveClicked(object obj)
        {
            await DataStore.SaveSetting(new SettingModel
            {
                EnableTranslation = IsTranslateEnabled,
                TranslatorIndex = SelectedTranslator.Index
            });

            GoToMain();
        }

        public async void GoToMain()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        public async void OnAppearing()
        {
            var setting = await DataStore.GetSetting();
            await LoadTranslators();

            SelectedTranslator = Translators.FirstOrDefault(t => t.Index == setting.TranslatorIndex);
            IsTranslateEnabled = setting.EnableTranslation;
        }

        private async Task LoadTranslators()
        {
            Translators.Clear();
            var fetchedTranslators = await DataStore.GetTranslates();
            foreach (var item in fetchedTranslators)
            {
                Translators.Add(item);
            }
        }
    }
}
