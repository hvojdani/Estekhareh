using Estekhareh.Models;
using Estekhareh.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{

    [QueryProperty(nameof(StartIndex), nameof(StartIndex))]
    public class ItemsViewModel : BaseViewModel
    {
        const int GETCOUNT = 7;
        private int _startIndex = 0;
        public string StartIndex
        {
            get
            {
                return _startIndex.ToString();
            }
            set
            {
                _startIndex = Convert.ToInt32(value);
            }
        }

        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();
            ItemTapped = new Command<Item>(OnItemSelected);
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var estekharehItems = await DataStore.GetItemsAsync(_startIndex, GETCOUNT);
                await DataStore.SaveLastIndex(_startIndex);

                foreach (var item in estekharehItems)
                {
                    Items.Add(item);
                }
                Title = Items.FirstOrDefault()?.AyaDescription;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            if (_startIndex == 0)
            {
                _startIndex = await DataStore.GetLastIndex();
                if (_startIndex == 0)
                    return;
            }
            await ExecuteLoadItemsCommand().ConfigureAwait(false);
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}