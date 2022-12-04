using MoneyTracking.Data;
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
        



        public ObservableCollection<SpendingTable> spendings { get; set; }
       


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
            spendings = new ObservableCollection<SpendingTable>();
           
            this.user = user;
            UserName = user.UserName;
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myQuery = db.Table<SpendingTable>().Where(u => u.UserId.Equals(user.UserId));
            
            foreach (var item in myQuery)
            {
                spendings.Add(new SpendingTable { Data = item.Data, Spent = item.Spent, Price = item.Price, UserId = item.UserId, SpendingId = item.SpendingId, Type=item.Type });
            }
            
         
            LogOutCommand = new Command(OnLogOutClicked);
            AddExpenseCommand = new Command(OnAddExpenseClicked);
          
        }

        async  private void OnAddExpenseClicked()
        {
            string name = await App.Current.MainPage.DisplayPromptAsync("Name", "On what did you spent money?");
            string price = await App.Current.MainPage.DisplayPromptAsync("Price", "How much did it cost?");
            string data = await App.Current.MainPage.DisplayPromptAsync("Data", "When did you buy it?");
            string type = "Expense";
            SpendingTable tempSpending = new SpendingTable {  SpendingId=Guid.NewGuid(), UserId = user.UserId, Data = data, Price = Convert.ToDouble(price), Spent = name, Type=type};
            if (database.InsertSpendingAsync(tempSpending) != null)
           {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                       var result = await App.Current.MainPage.DisplayAlert("Sucess", "Added sucesfully the spent", "Ok", "-");

                   });
                }
        }

        private void OnLogOutClicked()
        {
            foreach(var item in spendings)
            {
                Console.WriteLine(item.UserId);
            }
            App.Current.MainPage = new NavigationPage(new LoginPage());

        
        }
    }
}
