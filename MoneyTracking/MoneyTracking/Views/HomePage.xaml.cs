using MoneyTracking.Tables;
using MoneyTracking.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTracking.Views
{
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
         
        public HomePage(RegUserTable user)
        {


            
            InitializeComponent();
          
          BindingContext = new HomeViewModel(user);
         

        }

        
    }
}