using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankConsole.Models;
using BankConsole.Data;


namespace BankConsole.Data
{
    internal interface IFileRepository
    {
        void SaveAccount(Account account);
        List<Account> GetAccounts();
        void UpdateAccount(List<Account> records);
    }
}
