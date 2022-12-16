using MoneyTracking.Data;
using MoneyTracking.Tables;
using MoneyTracking.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MoneyTracking.ViewModels
{
    public class AddEarningViewModel : INotifyPropertyChanged
    {

        private string spent;
        private string price;
        private string data;



        DataBase database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db"));
        private RegUserTable user;
        public RegUserTable User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        public string Spent
        {
            get { return spent; }
            set
            {
                spent = value;
                OnPropertyChanged();
            }
        }


        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }



        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged();
            }
        }

        public Command AddEarnCommand { get; }
        public Command BackCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        public AddEarningViewModel(RegUserTable user)
        {
            this.user = user;
            AddEarnCommand = new Command(OnAddExpenseClicked);
            BackCommand = new Command(BackClicked);
        }
        async private void OnAddExpenseClicked()
        {

            SpendingTable tempSpending = new SpendingTable { SpendingId = Guid.NewGuid(), UserId = user.UserId, Data = data, Price= price, Spent = spent, Type = "Income", ImageUrl = "plus.png" };
            if (database.InsertSpendingAsync(tempSpending) != null)
            {

                App.Current.MainPage = new HomePage(user);
            }
        }
        private void BackClicked()
        {
            App.Current.MainPage = new HomePage(user);
        }
    }
}
