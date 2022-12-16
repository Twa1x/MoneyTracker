using MoneyTracking.Tables;
using MoneyTracking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSpentPage : ContentPage
    {
        public AddSpentPage(RegUserTable user)
        {
            InitializeComponent();
            BindingContext = new AddSpentViewModel(user);
        }
    }
}