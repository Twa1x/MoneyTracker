﻿using MoneyTracking.Data;
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
    public class HomeViewModel
    {

        private string userName;
        DataBase database = new DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db"));

        private string spent;
        private double price;
        private string data;

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

        public string Spent
        {
            get { return spent; }
            set
            {
                spent = value;
                OnPropertyChanged();
            }
        }

        public double Price
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
        public Command LogOutCommand { get; }
        RegUserTable user;
        public HomeViewModel(RegUserTable user)
        {
            UserName = user.UserName;
            LogOutCommand = new Command(OnLogOutClicked);
        }

        private void OnLogOutClicked()
        {
            Console.WriteLine(spent);
            Console.WriteLine(price);
            Console.WriteLine(data);

            SpendingTable tempSpending = new SpendingTable { UserId = user.UserId, Data = data, Price = price, Spent = spent };

            if(database.InsertSpendingAsync(tempSpending)!=null)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await App.Current.MainPage.DisplayAlert("Sucess", "Added sucesfully the spent", "Ok","");
                  

                });
            }
            else
            {
                Console.WriteLine("failed");
            }
        }
    }
}
