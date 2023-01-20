using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankConsole.Models;

namespace BankConsole.Data
{
    public interface IBank
    {
        string BankName { get; }
        List<Account> AccountList { get; set; }
        Account CreateAccount(string name);
        decimal Deposit(decimal amount, Account account);
        decimal Withdraw(decimal amount, Account account);
        void ChargeInterest();
        decimal Balance();
    }
}
