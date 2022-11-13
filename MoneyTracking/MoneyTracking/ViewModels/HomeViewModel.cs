using MoneyTracking.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MoneyTracking.ViewModels
{
    public class HomeViewModel
    {

        private string userName;
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
        RegUserTable user;
        public HomeViewModel(RegUserTable user)
        {
            UserName = user.UserName;
        }
    }
}
