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

    [QueryProperty(nameof(StartAya), nameof(StartAya))]
    public class ItemsViewModel : BaseViewModel
    {
        const int GETCOUNT = 7;
        private int _startAya = 2;
        public string StartAya
        {
            get
            {
                return _startAya.ToString();
            }
            set
            {
                _startAya = Convert.ToInt32(value);
                if (_startAya != 0)
                {
                    ExecuteLoadItemsCommand().ConfigureAwait(false);
                }
            }
        }


        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var estekharehItems = await DataStore.GetItemsAsync(_startAya, GETCOUNT);

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


        //public void OnAppearing()
        //{
            //if (Items.Count == 0)
            //{
            //    IsBusy = true;
            //    SelectedItem = null;
            //    await ExecuteLoadItemsCommand().ConfigureAwait(false);
            //}
        //}

        public async void OnBackButtonPressed()
        {
            await Shell.Current.GoToAsync($"//{ nameof(AboutPage)}", true);
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

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}