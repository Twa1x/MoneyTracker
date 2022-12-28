using MoneyTracking.Data;
using MoneyTracking.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracking
{
    public partial class App : Application
    {
       

        public App()
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
