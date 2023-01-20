using BankConsole.Data;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsole.Models
{
    public class Account
    {
        [Name("AccountID"), TypeConverter(typeof(Int32Converter))]
        public int AccountID { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("Balance"), TypeConverter(typeof(DecimalConverter))]
        public decimal Balance { get; set; } = 0;
        [Name("AccountType"), TypeConverter(typeof(Int32Converter))]
        public int AccountType { get; set; } = 1; 
        /*
         1 = Løn konto
         2 = Opsparings konto
         3 = Forbrugs konto
         */
    }
}
