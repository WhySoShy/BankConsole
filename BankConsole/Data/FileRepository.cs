using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankConsole.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace BankConsole.Data
{
    internal class FileRepository : IFileRepository
    {
        private string _fileName { get; } = @"C:\Users\emilk\source\repos\BankConsole\Data\data.csv";

        /// <summary>
        /// Gemmer alle brugerne i csv fil
        /// </summary>
        /// <param name="account"></param>
        public void SaveAccount(Account account)
        {
            bool exist = File.Exists(_fileName);
            using (StreamWriter writer = File.AppendText(_fileName))
            {

                if (!exist)
                    writer.WriteLine("AccountID;Name;Balance;AccountType");
                    
            writer.WriteLine($"{account.AccountID};{account.Name};{account.Balance};{account.AccountType};");
            }
        }
        /// <summary>
        /// Henter alle brugerne fra csv fil
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            CsvConfiguration _ = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };

            if (!File.Exists(_fileName))
                SaveAccount(new Account { AccountID = 0, AccountType = 1, Balance = 0, Name = "test" });
         
            using (var reader = new StreamReader(_fileName))
            using (var csv = new CsvReader(reader, _))
            {
                var records = csv.GetRecords<Account>();

                return new List<Account>(records);
            }
        }
        /// <summary>
        /// Opdatere alle brugerne i csv filen
        /// </summary>
        /// <param name="records"></param>
        public void UpdateAccount(List<Account> records)
        {
            File.Delete(_fileName);
            foreach(var record in records)
            {
                SaveAccount(record);
            }
        }
    }
}
