using EncryptKeyCient.UWP.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EncryptKeyCient.UWP.Views
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