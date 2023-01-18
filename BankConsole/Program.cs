using BankConsole.Data;
using BankConsole.Models;

//Indeholder både Bank1 & Bank2 pga et fuckup med github

///TODO:
/// * FIX SÅ DEN IKKE CRASHER NÅR DU TRYKKER ENTER
/// 


Menu();

static void CreateMenu(int accountID)
{
    Bank bank = new();
    Dictionary<char, string> menuItems = new Dictionary<char, string>();
    menuItems.Add('A', "Opret konto");
    menuItems.Add('B', "Indsæt");
    menuItems.Add('C', "Hæv");  
    menuItems.Add('D', "Vis saldo");
    menuItems.Add('E', "Vis bank");
    menuItems.Add('X', "Afslut");

    Console.Clear();
    Console.WriteLine($"{bank.BankName} | Logget på som: {bank.AccountList.Find(x => x.AccountID == accountID).Name}");
    for (int i = 0; i < bank.BankName.Length; i++)
        Console.Write("-");
    Console.WriteLine("\n");
    foreach(var item in menuItems)
        Console.WriteLine($"{item.Key}: {item.Value}");
}

static decimal CheckForDecimal(string amount, int accountID)
{
    if (Convert.ToDecimal(amount) <= 0)
    {
        Console.WriteLine($"Du kan ikke hæve / indsætte {amount}");
        return Convert.ToDecimal(amount);
    }
    while (!decimal.TryParse(amount, out decimal value))
    {
        CreateMenu(accountID);
        Console.WriteLine("\nIkke et decimal \n");
        Console.Write("Indsæt: ");
        amount = Console.ReadLine();
    }
    return Convert.ToDecimal(amount);
}

static void Menu()
{
    Bank bank = new();
    Account account = new();
    decimal amount = 0;
    int loggedInAs;
 
    
    Console.Write($"Opret en bruger: ");
    bank.CreateAccount(Console.ReadLine());

    Console.Write("Indtast dit navn: ");
    string name = Console.ReadLine();
    while (!bank.AccountList.Any(x => x.Name.ToLower() == name.ToLower()))
    {
        Console.WriteLine("Det blev ikke fundet nogen bruger med dette navn");
        Console.Write("Indtast dit navn: ");
        name = Console.ReadLine();
    }
    loggedInAs = bank.AccountList.Find(x => x.Name == name.ToLower()).AccountID;

    while (true)
    {
        CreateMenu(loggedInAs);


        var selectedItem = Console.ReadKey(true);

        switch (selectedItem.Key)
        {
            case ConsoleKey.A:
                Console.Write("Opret bruger: ");
                account = bank.CreateAccount(Console.ReadLine());
                break;
            case ConsoleKey.B:
                Console.Write("Indtast hvor meget du vil indsætte: ");
                amount = CheckForDecimal(Console.ReadLine(), loggedInAs);
                if (amount > 0)
                Console.WriteLine($"Du har indsat {amount}kr | Ny saldo {bank.Deposit(amount, loggedInAs)}kr");

                break;
            case ConsoleKey.C:
                Console.Write("Indtast hvor meget du vil hæve: ");
                amount = CheckForDecimal(Console.ReadLine(), loggedInAs);
                
                if (account.Balance - amount >= 0)
                {
                    bank.Withdraw(amount, loggedInAs);
                    break;
                }
                Console.WriteLine($"Du mangler {amount - account.Balance}kr for at kunne trække dette antal ud! | Saldo {account.Balance}kr");
                break;
            case ConsoleKey.D:
                Console.WriteLine($"Nuværende saldo: {bank.AccountList.Find(x => x.Name.ToLower() == name.ToLower()).Balance}kr");
                break;
            case ConsoleKey.E:
                Console.WriteLine(bank.BankName);
                Console.WriteLine($"Der er {bank.Balance}kr i hele banken");
                break;
            case ConsoleKey.X:
                break;

            default:
                break;
        }


        Console.WriteLine("Tryk på en knap for at gå vidre" + Console.ReadLine());
    }
}





