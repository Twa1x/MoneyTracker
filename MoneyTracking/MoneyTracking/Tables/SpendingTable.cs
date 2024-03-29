﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoneyTracking.Tables
{
    public class SpendingTable
    {
        [PrimaryKey]
        public Guid SpendingId { get; set; }
        public Guid UserId { get; set; }
        public string Spent { get; set; }
        public string Price { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }


    }
}
