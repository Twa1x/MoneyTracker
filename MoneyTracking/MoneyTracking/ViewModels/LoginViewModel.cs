using MoneyTracking.Data;
using MoneyTracking.Tables;
using MoneyTracking.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MoneyTracking.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        UserDataBase database = new UserDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db"));

        private string userName;
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }


        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }


        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; }
        public Command SignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private  void OnLoginClicked()
        {

            Console.WriteLine(userName);
            Console.WriteLine(password);
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(userName) && u.Password.Equals(password)).FirstOrDefault();
            Console.WriteLine(myQuery);
            if (myQuery != null)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await App.Current.MainPage.DisplayAlert("Error", "Credentials Wrong", "Yes", "Cancel");
                    if (result)
                        App.Current.MainPage = new NavigationPage(new LoginPage());
                    else
                        App.Current.MainPage = new NavigationPage(new LoginPage());

                });
            }
        }

        private void OnSignUpClicked()
        {
            App.Current.MainPage = new NavigationPage(new RegistrationPage());
        }
    }
}
