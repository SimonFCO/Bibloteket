//Simon Brink Sut25

using System.Globalization;

namespace Bibloteket
{
    class books{

    }
    internal class Program
    {

        //This is the start menu where you can choose what to do in the program
        static void startMenu()
        {
            Console.Clear();
            Console.WriteLine("--- Bibloteket ---");
            Console.WriteLine("[A]: Login");
            Console.WriteLine("[B]: Exit");
            string answer = Console.ReadLine();
            //If a continue to the login screen if b then just quit
            if(answer == "A" || answer == "a")
            {
                tryLogin();
            }else if(answer == "B" || answer == "b")
            {
                softwareIsAlive = false;
                loggedIn = false;
            }
            else
            {
                Console.WriteLine("Försök igen");
                Console.ReadLine();
                startMenu();
            }
        }
        //This is the function for the login screen :D
        static void tryLogin()
        {
            string loginUsername = "";
            Console.WriteLine("--- Login ---");
            Console.WriteLine("Username: ");
            bool valid = false;
            while(valid == false)
            {
                loginUsername = Console.ReadLine();
                for (int i = 0; i < usernames.Length; i++)
                {
                    if (loginUsername == usernames[i])
                    {
                        valid = true;
                    }
                }

                if (valid == false)
                {
                    Console.WriteLine("Inte ett registrerat användarnamn");
                }
            }

            int maxAttempts = 3;
            int loginPin = 0;
            loginAttemps = 0;

            while (softwareIsAlive && !loggedIn && loginAttemps < maxAttempts)
            {
                loginAttemps++;
                Console.Write("Pinkod: ");
                try
                {
                    loginPin = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                Console.WriteLine("svaret var inte ett heltal.");
                continue; // count as an attempt and loop again
                }
                catch (OverflowException)
                {
                Console.WriteLine("Nummret var antingen för stort eller för litet.");
                continue;
                }

            // check if the username and pincode match
            for (int i = 0; i < usernames.Length; i++)
                {
                    if (loginUsername == usernames[i] && loginPin == pinCode[i])
                    {
                        loggedIn = true;
                        softwareIsAlive = true;
                        Console.WriteLine($"Inloggad som {loginUsername}");
                        navigationMenu();
                        break;
                    }
                }
                //if not the correct pincode then write how many tries left
                if (!loggedIn && softwareIsAlive == true)
                {
                    if (loginAttemps >= maxAttempts)
                    {
                        Console.WriteLine("För många försök, stänger av.");
                        // take action: disable software, exit loop, etc.
                        softwareIsAlive = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"misslyckad inlogning — {maxAttempts - loginAttemps} försök kvar.");
                    }
                }

            }



        }

        static void pressEnterToReturn()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tryck Enter för att återgå till huvudmenyn");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        
        //This is the navigationMenu Where after your login you can choose what to do!
        static void navigationMenu(){

          Console.Clear();
          Console.WriteLine("1, Visa Böcker");
          Console.WriteLine("2, Låna bok");
          Console.WriteLine("3, Lämna tillbaka bok");
          Console.WriteLine("4, Mina lån");
          Console.WriteLine("5, Logga ut");

          string answer = Console.ReadLine();

          switch (answer)
          {
              case "1":
              showBooks();
              break;
              case "2":
              borrowBooks();
              break;
              case "3":
              returnBooks();
              break;
              case "4":
              myBorrowedBooks();
              break;
              case "5":
                    loggedIn = false;
              startMenu();
              break;
              default:
              Console.WriteLine("ogiltigt val");
              navigationMenu();
              break;
          }


        }
        //This function here will show what books you have, nothing more nothing less
        static void showBooks(){
          for (int i = 0; i < bookTitle.Length; i++)
          {
              Console.WriteLine($"{i+1} | Titel: {bookTitle[i]} | Exemplar: {aviableBooks[i]}");
          }
            pressEnterToReturn();

        }

        //This will let the user borrow the books that he chooses
        static void borrowBooks(){
          Console.WriteLine("Vilken bok vill du låna? Skriv nummret av boken du vill låna");
            for (int i = 0; i < bookTitle.Length; i++)
            {
                Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]} | Exemplar: {aviableBooks[i]}");
            }
            int answer = Convert.ToInt32(Console.ReadLine());


            if (aviableBooks[answer - 1] != 0){
            aviableBooks[answer-1] -= 1;
                ammountOfBorrowedBooks[answer - 1] += 1;
                isBookBorrowed[answer - 1] = true;

                Console.WriteLine($"Du har lånat | Titel: {bookTitle[answer-1]}");
          }else{
            Console.WriteLine("Du kunde inte låna boken, det kanske inte finns några kvar");
          }
            pressEnterToReturn();

        }

        static void returnBooks(){
            for(int i = 0; i<isBookBorrowed.Length; i++)
            {
                if (isBookBorrowed[i] == true)
                {
                    Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]}");
                }
            }
            Console.WriteLine("Vilken bok vill du Lämna tillbaka? Skriv nummret av boken du vill låna");
            int answer = Convert.ToInt32(Console.ReadLine());
            if (isBookBorrowed[answer - 1] == true)
            {
                aviableBooks[answer - 1] += 1;
                ammountOfBorrowedBooks[answer - 1] -= 1;
                if (ammountOfBorrowedBooks[answer - 1] == 0)
                {
                    isBookBorrowed[answer - 1] = false;
                }
                
            }
            else
            {
                Console.WriteLine("Du har inte lånat denna boken så du kan inte lämna tillbaka den.");
            }
            pressEnterToReturn();
        }

        static void myBorrowedBooks(){
            for (int i = 0; i < isBookBorrowed.Length; i++)
            {
                if (isBookBorrowed[i] == true)
                {
                    Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]} | antal: {ammountOfBorrowedBooks[i]}");
                }
            }
            pressEnterToReturn();
        }



        //These are where most of the variables are stored
        static int loginAttemps = 0;
        static string[] usernames = {"Bengt","Stig","Rutger","Sten", "Harald"};
        static int[] pinCode = {123456, 234567, 345678, 987654, 876554 };
        static bool loggedIn;
        static bool softwareIsAlive = true;

        //Book variables
        static string[] bookTitle = {"Harry's potter", "Urtaskig park", "sjärn krig", "stump fiction", "Vargen av väggvägen"};
        static int[] aviableBooks = {1, 10, 0, 5, 2};
        static bool[] isBookBorrowed = {false, false, false, false, false};
        static int[] ammountOfBorrowedBooks = { 0, 0, 0, 0, 0 };

        //Main Function
        static void Main(string[] args)
        {
            startMenu();
            while (softwareIsAlive == true && loggedIn == true){
              navigationMenu();
            }
            
            
        }
    }
}
