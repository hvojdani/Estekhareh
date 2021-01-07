using Estekhareh.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Estekhareh.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}