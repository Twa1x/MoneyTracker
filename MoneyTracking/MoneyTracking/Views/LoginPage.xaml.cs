using MoneyTracking.Tables;
using MoneyTracking.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new RegistrationPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(EntryUser.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();
            if (myQuery != null)
            {
                App.Current.MainPage = new NavigationPage(new AboutPage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Credentials Wrong", "Yes", "Cancel");
                    if (result)
                        App.Current.MainPage = new NavigationPage( new LoginPage());
                    else
                        App.Current.MainPage = new NavigationPage( new LoginPage());

                });
            }
        }
    }
}