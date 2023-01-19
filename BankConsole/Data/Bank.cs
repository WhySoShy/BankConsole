using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankConsole.Models;

namespace BankConsole.Data
{
    public class Bank
    {
        public string BankName { get; } = "Et eller andet bank navn";
        public List<Account> AccountList { get; set; } = new List<Account>();

        public Account CreateAccount(string name)
        {
            if (AccountList.Any(x => x.Name.ToLower() == name.ToLower()))
                throw new Exception("Bruger eksistere allerede");
            Account account = new()
            {
                Balance = 0,
                AccountID = AccountList.Count + 1,
                Name = name,
            };
            AccountList.Add(account);

            return account;
        }

        public decimal Deposit(decimal amount, int accountID) =>
            AccountList.Find(x => x.AccountID == accountID).Balance += amount;


        public decimal Withdraw(decimal amount, int accountID) =>
            AccountList.Find(x => x.AccountID == accountID).Balance -= amount;

        /// <returns>Account Balance</returns>
        public decimal Balance()
        {
            decimal allBalance = 0;
            foreach (var item in AccountList)
                allBalance += item.Balance;
            return allBalance;
        }
    }
}
