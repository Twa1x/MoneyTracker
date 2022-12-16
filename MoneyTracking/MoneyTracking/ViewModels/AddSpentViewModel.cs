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
    public class AddSpentViewModel : INotifyPropertyChanged
    {

        private string spent;
        private string price;
        private string data;

        private string modifyString(string str)
        {


            return str;
        }

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
        public Command BackCommand { get; }
        public Command AddExpenseCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
        public AddSpentViewModel(RegUserTable user)
        {
            this.user = user;
            AddExpenseCommand = new Command(OnAddExpenseClicked);
            BackCommand = new Command(BackClicked);
        }
        async private void OnAddExpenseClicked()
        {

            SpendingTable tempSpending = new SpendingTable { SpendingId = Guid.NewGuid(), UserId = user.UserId, Data = data, Price= price, Spent = spent, Type = "Expense", ImageUrl = "minus.png" };
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
