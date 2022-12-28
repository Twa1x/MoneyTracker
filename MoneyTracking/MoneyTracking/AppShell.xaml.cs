using MoneyTracking.ViewModels;
using MoneyTracking.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MoneyTracking
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AppShell");
        }
    }
}
