using BankConsole.Data;
using BankConsole.Models;

///TODO:
/// * FIX SÅ DEN IKKE CRASHER NÅR DU TRYKKER ENTER
/// 
namespace BankConsole; 
class Program
{
    public static void Main()
    {
        Bank bank = new();

        Startup();

        void CreateMenu(string name)
        {
            Dictionary<char, string> menuItems = new Dictionary<char, string>();
            menuItems.Add('A', "Opret konto");
            menuItems.Add('B', "Indsæt");
            menuItems.Add('C', "Hæv");
            menuItems.Add('D', "Vis saldo");
            menuItems.Add('E', "Vis bank");
            menuItems.Add('X', "Logud");

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

            bank.CreateAccount("test");

            while (true)
            {
                Console.Clear();
                if (loggedInAs == null)
                {
                    Console.Write("Indtast dit navn: ");
                    name = Console.ReadLine();
                    loggedInAs = bank.AccountList.Find(x => x.Name.ToLower() == name.ToLower());
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
                            bank.Deposit(amount, loggedInAs.AccountID);
                            Console.WriteLine($"Du har indsat {amount}kr | Ny saldo {loggedInAs.Balance}kr");
                            break;
                        }

                        break;
                    case ConsoleKey.C:
                        Console.Write("Indtast hvor meget du vil hæve: ");
                        amount = Convert.ToDecimal(Console.ReadLine());
                        if (CheckForDecimal(amount.ToString(), loggedInAs.Name) - loggedInAs.AccountID  >= 0)
                        {
                            bank.Withdraw(amount, loggedInAs.AccountID);
                            Console.WriteLine($"Du har hævet {amount}kr | Ny saldo {loggedInAs.Balance}kr");
                            break;
                        }
                        Console.WriteLine($"Du mangler {amount - loggedInAs.Balance}kr for at kunne trække dette antal ud! | Saldo {loggedInAs.Balance}kr");
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine($"Nuværende saldo: {bank.AccountList.Find(x => x.Name.ToLower() == name.ToLower()).Balance}kr");
                        break;
                    case ConsoleKey.E:
                        Console.WriteLine(bank.BankName);
                        Console.WriteLine($"Der er {bank.Balance}kr i hele banken");
                        break;
                    case ConsoleKey.X:
                        loggedInAs = null;
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
