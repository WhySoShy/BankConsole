using BankConsole.Data;
using BankConsole.Models;
using BankConsole.Util;

///TODO:
/// * FIX SÅ DEN IKKE CRASHER NÅR DU TRYKKER ENTER
/// 
namespace BankConsole; 

class Program
{
    public static void Main()
    {
        Bank bank = new();
        FileRepository fileRepository = new FileRepository();

        Startup();

        void CreateMenu(string name)
        {
            Dictionary<char, string> menuItems = new Dictionary<char, string>();
            menuItems.Add('A', "Opret konto");
            menuItems.Add('B', "Indsæt");
            menuItems.Add('C', "Hæv");
            menuItems.Add('D', "Vis saldo");
            menuItems.Add('E', "Vis bank");
            menuItems.Add('F', "Charge interest");
            menuItems.Add('G', "Hvis alle kontier");
            menuItems.Add('H', "Vis Log");
            menuItems.Add('I', "Log ud");
            menuItems.Add('J', "Afslut");

            Console.Clear();
            Console.WriteLine($"{bank.BankName} | Logget på som: {name}");
            for (int i = 0; i < bank.BankName.Length + name.Length; i++)
                Console.Write("-");
            Console.WriteLine("\n");
            foreach (var item in menuItems)
                Console.WriteLine($"{item.Key}: {item.Value}");
        }

        decimal CheckForDecimal(string amount, string name)
        {
            if (Convert.ToDecimal(amount) <= 0)
            {
                Console.WriteLine($"Du kan ikke hæve / indsætte {amount}");
                return Convert.ToDecimal(amount);
            }
            while (!decimal.TryParse(amount, out decimal value))
            {
                CreateMenu(name);
                Console.WriteLine("\nIkke et decimal \n");
                Console.Write("Indsæt: ");
                amount = Console.ReadLine();
            }
            return Convert.ToDecimal(amount);
        }

        void Startup()
        {

            decimal amount = 0;
            Account loggedInAs = null;
            string name = String.Empty;

            //bank.CreateAccount("test");

            while (true)
            {
                
                Console.Clear();
                if (loggedInAs == null)
                {
                    Console.Write("Indtast dit navn: ");
                    bank.AccountList = fileRepository.GetAccounts();
                    name = Console.ReadLine();
                    loggedInAs = bank.AccountList.Find(x => x.Name.ToLower() == name.ToLower()) /*bank.AccountList.Find(x => x.Name.ToLower() == name.ToLower())*/;
                    continue;
                }
                CreateMenu(loggedInAs.Name);

                var selectedItem = Console.ReadKey(true);

                switch (selectedItem.Key)
                {
                    case ConsoleKey.A:
                        Console.Write("Opret bruger: ");
                        bank.CreateAccount(Console.ReadLine());
                        break;
                    case ConsoleKey.B:
                        Console.Write("Indtast hvor meget du vil indsætte: ");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        if (CheckForDecimal(amount.ToString(), loggedInAs.Name) > 0)
                        {
                            bank.Deposit(amount, loggedInAs);
                            Console.WriteLine($"Du har indsat {amount}kr | Ny saldo {loggedInAs.Balance}kr");
                            break;
                        }

                        break;
                    case ConsoleKey.C:
                        Console.Write("Indtast hvor meget du vil hæve: ");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        if (CheckForDecimal(amount.ToString(), loggedInAs.Name) - loggedInAs.AccountID  >= 0)
                        {
                            try
                            {
                                bank.Withdraw(amount, loggedInAs);
                                Console.WriteLine($"Du har hævet {amount}kr | Ny saldo {loggedInAs.Balance.ToString().Replace(",", ".")}kr");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine($"Nuværende saldo: {loggedInAs.Balance.ToString().Replace(",", ".")}kr");
                        break;
                    case ConsoleKey.E:
                        foreach (var item in bank.AccountList)
                            Console.WriteLine($"{item.Name} | ID: {item.AccountID} | Bal: {item.Balance.ToString().Replace(",", ".")} | Type: {item.AccountType}");
                        break;
                    case ConsoleKey.G:
                        foreach (var item in bank.AccountList)
                            Console.WriteLine($"Kontornr: {item.AccountID}\n Navn: {item.Name}\n KontoType: {(AccountType) Enum.ToObject(typeof(AccountType), item.AccountID)} konto\n Saldo: {item.Balance} \n-------");
                        break;
                    case ConsoleKey.X:
                        loggedInAs = null;
                        break;
                    case ConsoleKey.F:
                        bank.ChargeInterest();
                        Console.WriteLine("Der blev givet renter.");
                        break;
                    case ConsoleKey.H:
                        Console.Clear();
                        Console.WriteLine("Log over bank historik\n");
                        Console.WriteLine(FileLogger.ReadFromLog());
                        break;
                    case ConsoleKey.I:
                        loggedInAs = null;
                        fileRepository.UpdateAccount(bank.AccountList);
                        break;
                    case ConsoleKey.J:
                        fileRepository.UpdateAccount(bank.AccountList);
                        Environment.Exit(200);
                        break;
                    default:
                        break;
                }
                if (selectedItem.Key != ConsoleKey.X)
                {
                    Console.WriteLine("Tryk på en knap for at gå vidre");
                    Console.ReadLine();
                }  
            }
        }




    }

}
