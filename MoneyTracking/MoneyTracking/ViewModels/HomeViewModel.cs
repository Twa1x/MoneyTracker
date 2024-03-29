﻿using MoneyTracking.Data;
using MoneyTracking.Tables;
using MoneyTracking.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoneyTracking.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        private string userName;



        public ObservableCollection<SpendingTable> incomes { get; set; }
        public ObservableCollection<SpendingTable> expenses { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }


        DataBase database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db"));


     

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }


        public Command LogOutCommand { get; }
        public Command AddExpenseCommand { get; }
        public Command AddMoneyCommand { get; }
        public Command TotalExpensesCommand { get; }
        public Command TotalIncomingsCommand { get; }
        public Command ResetCommand { get; }


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
        public HomeViewModel(RegUserTable user)
        {
            incomes = new ObservableCollection<SpendingTable>();
            expenses = new ObservableCollection<SpendingTable>();

            this.user = user;
            UserName = user.UserName;
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<SpendingTable>().Where(u => u.UserId.Equals(user.UserId));
           
            foreach (var item in myQuery)
            {
                if(item.Type == "Expense")
                    expenses.Add(new SpendingTable { Data = item.Data, Spent = item.Spent, Price = item.Price, UserId = item.UserId, SpendingId = item.SpendingId, Type = item.Type, ImageUrl=item.ImageUrl });
                else
                    incomes.Add(new SpendingTable { Data = item.Data, Spent = item.Spent, Price = item.Price, UserId = item.UserId, SpendingId = item.SpendingId, Type = item.Type, ImageUrl = item.ImageUrl });
            }

            

            LogOutCommand = new Command(OnLogOutClicked);
            AddExpenseCommand = new Command(OnAddExpenseClicked);
            AddMoneyCommand = new Command(OnAddMoneyClicked);
            TotalIncomingsCommand = new Command(OnCheckIncomingsClicked);
            TotalExpensesCommand = new Command(OnCheckExpensesClicked);
            ResetCommand = new Command(ResetClicked);
        }

        private void ResetClicked(object obj)
        {
            
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<SpendingTable>().Where(u => u.UserId.Equals(user.UserId));
            foreach (var item in myQuery)
               db.Delete(item);
        }

        async private void OnCheckExpensesClicked(object obj)
        {
            double totalSum = 0;
            foreach (var item in expenses)
            {
                totalSum = totalSum + Convert.ToDouble(item.Price);
            }
            await App.Current.MainPage.DisplayAlert("Total expenses", totalSum.ToString()+"$", "OK");
        }

        async private void OnCheckIncomingsClicked(object obj)
        {
            double totalSum = 0;
            foreach(var item in incomes)
            {
                totalSum = totalSum + Convert.ToDouble(item.Price);
            }
            await App.Current.MainPage.DisplayAlert("Total incomes", totalSum.ToString()+"$", "OK");
        }

        async private void OnAddExpenseClicked()
        {
            App.Current.MainPage = new NavigationPage(new AddSpentPage(user));
        }
        async private void OnAddMoneyClicked()
        {

            App.Current.MainPage = new NavigationPage(new AddEarnPage(user));
        
        }
        private void OnLogOutClicked()
        {
            foreach (var item in incomes)
            {
                Console.WriteLine(item.UserId);
            }
            App.Current.MainPage = new NavigationPage(new LoginPage());


        }
    }
}
