using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Estekhareh.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private string description;
        private string ayaDescription;
        public int Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string AyaDescription
        {
            get => ayaDescription;
            set => SetProperty(ref ayaDescription, value);
        }

        public string ItemId
        {
            get
            {
                return itemId.ToString();
            }
            set
            {
                itemId = Convert.ToInt32(value);
                LoadItemId(itemId);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                AyaDescription = item.AyaDescription;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
