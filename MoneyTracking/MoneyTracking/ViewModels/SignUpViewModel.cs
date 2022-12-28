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
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyTracking.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        DataBase database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db"));

        private string userName;
        private string password;
        private string email;

        public Command RegisterCommand { get; }
        public Command BackCommand { get; }

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

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }


        public SignUpViewModel()
        {

            RegisterCommand = new Command(Register);
            BackCommand = new Command(Back);
        }

        private void Back()
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        private void Register()
        {

            RegUserTable tempUser = new RegUserTable { UserId = Guid.NewGuid(), UserName = userName, Password = password, Email = email };

            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(UserName));
            var myQuery2 = db.Table<RegUserTable>().Where(u => u.Email.Equals(Email));

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if(!match.Success)
            {
                var result = App.Current.MainPage.DisplayAlert("Failed", "Email format xyz@xyz.xyz", "Try AGAIN", "Cancel");
            }
            else
            if (myQuery.Count() == 0 && myQuery2.Count() == 0)
            {
                if (database.InsertUserAsync(tempUser) != null)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var result = await App.Current.MainPage.DisplayAlert("Sucess", "Signed Up sucessfully!", "Login", "Cancel");
                        if (result)
                            App.Current.MainPage = new NavigationPage(new LoginPage());
                        else
                            App.Current.MainPage = new NavigationPage(new RegistrationPage());

                    });
                }
            }

            else
            {
                var result = App.Current.MainPage.DisplayAlert("Failed", "Username or email already exists!", "Try AGAIN", "Cancel");
            }

        }

    }
}
