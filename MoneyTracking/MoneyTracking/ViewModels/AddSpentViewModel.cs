using MoneyTracking.Data;
using MoneyTracking.Tables;
using MoneyTracking.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MoneyTracking.ViewModels
{
    public class AddSpentViewModel : INotifyPropertyChanged
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

            SpendingTable tempSpending = new SpendingTable { SpendingId = Guid.NewGuid(), UserId = user.UserId, Data = data, Price = price, Spent = spent, Type = "Expense", ImageUrl = "minus.png" };

            Regex regex = new Regex("^[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4}$");
            Regex regexPrice = new Regex("^[0-9]*[.]*[0-9]*$");
            Match match = regex.Match(Data);
            Match matchPrice = regexPrice.Match(Price);

            if (!matchPrice.Success)
            {

                var result = App.Current.MainPage.DisplayAlert("Failed", "ONLY NUMBERS", "Try AGAIN", "Cancel");
            }
            else
            if (!match.Success)
            {
                var result = App.Current.MainPage.DisplayAlert("Failed", "DATA format DD/MM/YYYY", "Try AGAIN", "Cancel");
            }
            else
            {
                await database.InsertSpendingAsync(tempSpending);
                App.Current.MainPage = new HomePage(user);
            }

        }
        private void BackClicked()
        {
            App.Current.MainPage = new HomePage(user);
        }
    }
}
