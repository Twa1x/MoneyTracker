using MoneyTracking.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoneyTracking.Views
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