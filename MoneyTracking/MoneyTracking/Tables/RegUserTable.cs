using MoneyTracking.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MoneyTracking.Tables
{
    public class RegUserTable
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
      
               
    }
}
