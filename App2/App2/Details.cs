using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Details
    {
        [PrimaryKey, AutoIncrement]
        public int MeterNo { get; set; }
        public string TypeOfRegistration { get; set; }
        public double PresentReading { get; set; }
        public double PreviousReading { get; set; }
        public double PrincipalAmount { get; set; }
        public double AmountPayable { get; set; }
    }
}