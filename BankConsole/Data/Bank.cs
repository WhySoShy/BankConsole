using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankConsole.Models;
using BankConsole.Util;

namespace BankConsole.Data
{
    public class Bank : IBank
    {
        public string BankName { get; } = "Et eller andet bank navn";
        public List<Account> AccountList { get; set; } = new List<Account>();

        // Ændre det til login med ID

        /// <returns>Retunere et helt Account object</returns>
        public Account CreateAccount(string name)
        {
            FileRepository _ = new();
            if (_.GetAccounts().Any(x => x.Name.ToLower() == name.ToLower()))
                throw new Exception("Bruger eksistere allerede");

            Account account = new();

            if (name != "test")
            {
                Console.WriteLine("A: Lønkonto");
                Console.WriteLine("B: OpsparingsKonto");
                Console.WriteLine("C: ForbrugsKonto");
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        account.AccountType = 1;
                        break;
                    case ConsoleKey.B:
                        account.AccountType = 2;
                        break;
                    case ConsoleKey.C:
                        account.AccountType = 3;
                        break;
                    default:
                        break;
                }
                var accounts = _.GetAccounts();
            }
            account.Name = name;
            account.AccountID = AccountList.Count() + 1;

            FileLogger.WriteToLog($"Ny {(AccountType)Enum.ToObject(typeof(AccountType), account.AccountID)}konto med kontonr: {account.AccountID} oprettet til {name}");

            AccountList.Add(account);

            return account;
        }
        /// <returns>Den nye balance</returns>
        public decimal Deposit(decimal amount, Account account)
        {
            FileRepository _ = new();

            account.Balance += amount;
            FileLogger.WriteToLog($"På kontonr: {account.Name} er der hævet {amount}kr og saldoen er nu {account.Balance}kr");

            return amount;
        }

        /// <returns>Den nye balance</returns>
        public decimal Withdraw(decimal amount, Account account)
        {
            if (account.Balance - amount < 0)
                throw new Exception("Du har ikke nok penge til at hæve "+amount);
            account.Balance -= amount;
            FileLogger.WriteToLog($"På kontonr: {account.Name} er der hævet {amount}kr og saldoen er nu {account.Balance}kr");

            return account.Balance;
        }

        /// <summary>
        /// Tager renter fra de forskellige typer Accounts
        /// </summary>
        public void ChargeInterest()
        {
            foreach(var item in AccountList)
                switch(item.AccountType)
                {
                    case 1:
                        item.Balance *= (decimal)1.05;
                        break;
                    case 2:
                        item.Balance *= Convert.ToDecimal(item.Balance <= 50 ? 1.01 : item.Balance <= 100 ? 1.02 : 1.03);
                        break;
                    case 3:
                        item.Balance *= Convert.ToDecimal(item.Balance >= 0 ? 1.001 : 0.80);
                        break;
                }
            FileLogger.WriteToLog("Renter tilskrevet");
        }

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
