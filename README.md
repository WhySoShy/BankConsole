# BankConsole

Projektet er lavet til at forestille sig en bank hvor du har 9 forskellige funktioner som bliver vist i en menu.

### Log
Oprettelse af brugere, Hæve og Indsætte penge ind i sin konti bliver logget samt hentning af renter.

---

Projektet består af 9 forskellige klasser.
  #### Data: 
  * Bank <br/>
  * FileRepository <br/>

  #### Interfaces:
  * IBank <br/>
  * IFileRepository <br/>
    
  #### Models:
  * Account <br/>
  * AccountType (Enum) <br/>
  
  #### Util:
  * FileLogger <br/>


#### Konto typer:
 * Løn konto
  * Giver 5% i rente
 * Opsparings konto
  * 1% i rente under 50kr
  * 2% i rente under 100kr
  * 3% i rente over 100kr 
 * Forbrugs konto
  * 0.1% i rente hvis kontoen ikke er i minus
  * -20% hvis kontoen er i minus
