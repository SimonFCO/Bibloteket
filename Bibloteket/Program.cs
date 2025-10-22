//Simon Brink Sut25

using System.Globalization;

namespace Bibloteket
{
    internal class Program
    {
        //These are where most of the variables are stored
        static int loginAttemps = 0;
        static string[] usernames = { "Bengt", "Stig", "Rutger", "Sten", "Harald" };
        static int[] pinCode = { 123456, 234567, 345678, 987654, 876554 };
        static bool loggedIn;
        static bool softwareIsAlive = true;

        //Book variables
        static string[] bookTitle = { "Harry's potter", "Urtaskig park", "sjärn krig", "stump fiction", "Vargen av väggvägen" };
        static int[] aviableBooks = { 1, 10, 0, 5, 2 };
        static int accountLoggedIn = 0;
        static bool[,] isBookBorrowed = { { false, false, false, false, false }, { false, false, false, false, false }, { false, false, false, false, false }, { false, false, false, false, false }, { false, false, false, false, false } };
        static int[,] ammountOfBorrowedBooks = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

        //Main Function
        static void Main(string[] args)
        {
            startMenu();
            while (softwareIsAlive && loggedIn)
            {
                navigationMenu(); 
            }


        }

        //This is the start menu where you can choose what to do in the program
        static void startMenu()
        {
            Console.Clear();
            Console.WriteLine("--- Bibloteket ---");
            Console.WriteLine("[A]: Login");
            Console.WriteLine("[B]: Exit");
            string answer = Console.ReadLine();
            //If a continue to the login screen if b then just quit
            if (answer == "A" || answer == "a")
            {
                tryLogin();
            }
            else if (answer == "B" || answer == "b")
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
            while (!valid)
            {
                loginUsername = Console.ReadLine().ToLower();
                for (int i = 0; i < usernames.Length; i++)
                {
                    if (loginUsername == usernames[i].ToLower())
                    {
                        accountLoggedIn = i;
                        valid = true;
                    }
                }

                if (!valid)
                {
                    Console.WriteLine("Inte ett registrerat användarnamn");
                }
            }

            int maxAttempts = 3;
            int loginPin = 0;
            loginAttemps = 0;
            //This will limit the ammount of login atemps to 3
            while (softwareIsAlive && !loggedIn && loginAttemps < maxAttempts)
            {
                loginAttemps++;
                Console.Write("Pinkod: ");
                loginPin = GetNumber(loginPin, 0, 1000000);

                // check if the username and pincode match
                for (int i = 0; i < usernames.Length; i++)
                {
                    if (loginUsername == usernames[i].ToLower() && loginPin == pinCode[i])
                    {
                        loggedIn = true;
                        softwareIsAlive = true;
                        Console.WriteLine($"Inloggad som {loginUsername}");
                        navigationMenu();
                        break;
                    }
                }
                //if not the correct pincode then write how many tries left
                if (!loggedIn && softwareIsAlive)
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

        //This will just output a little colored text saying press enter to return to main menu
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
        static void navigationMenu()
        {

            Console.Clear();
            Console.WriteLine("1, Visa Böcker");
            Console.WriteLine("2, Låna bok");
            Console.WriteLine("3, Lämna tillbaka bok");
            Console.WriteLine("4, Mina lån");
            Console.WriteLine("5, Logga ut");

            string answer = Console.ReadLine();

            //This will depending on the answer start the corresponding function
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
        static void showBooks()
        {
            for (int i = 0; i < bookTitle.Length; i++)
            {
                Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]} | Exemplar: {aviableBooks[i]}");
            }
            pressEnterToReturn();

        }

        //This will let the user borrow the books that he chooses
        static void borrowBooks()
        {
            Console.WriteLine("Vilken bok vill du låna? Skriv nummret av boken du vill låna");
            for (int i = 0; i < bookTitle.Length; i++)
            {
                Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]} | Exemplar: {aviableBooks[i]}");
            }

            int answer = 0; 
            answer = GetNumber(answer, 1, 5);

            //This is the mechanism for borrowing. It will remove one from how many books are aviable to borrow and make the book borrowed
            if (answer >= 1 && answer <= aviableBooks.Length && aviableBooks[answer - 1] != 0)
            {
                aviableBooks[answer - 1] -= 1;
                ammountOfBorrowedBooks[accountLoggedIn, answer - 1] += 1;
                isBookBorrowed[accountLoggedIn, answer - 1] = true;

                Console.WriteLine($"Du har lånat | Titel: {bookTitle[answer - 1]}");
            }
            else
            {
                Console.WriteLine("Du kunde inte låna boken, det kanske inte finns några kvar");
            }
            pressEnterToReturn();

        }

        //This will open the menu for returning books, it will show what books are currently borrowed and some information about them
        static void returnBooks()
        {
            for (int i = 0; i < bookTitle.Length; i++)
            {
                if (isBookBorrowed[accountLoggedIn, i])
                {
                    Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]}");
                }
            }
            Console.WriteLine("Vilken bok vill du Lämna tillbaka? Skriv nummret av boken du vill låna");
            int answer = 0;
            answer = GetNumber(answer, 0, 5);
            if (answer >= 1 && answer <= isBookBorrowed.Length && isBookBorrowed[accountLoggedIn, answer - 1])
            {
                aviableBooks[answer - 1] += 1;
                ammountOfBorrowedBooks[accountLoggedIn, answer - 1] -= 1;
                if (ammountOfBorrowedBooks[accountLoggedIn, answer - 1] == 0)
                {
                    isBookBorrowed[accountLoggedIn, answer - 1] = false;
                }

            }
            else
            {
                Console.WriteLine("Du har inte lånat denna boken så du kan inte lämna tillbaka den.");
            }
            pressEnterToReturn();
        }

        //This will give out a list of all books that are borrowed
        static void myBorrowedBooks()
        {
            for (int i = 0; i < bookTitle.Length; i++)
            {
                if (isBookBorrowed[accountLoggedIn, i])
                {
                    Console.WriteLine($"{i + 1} | Titel: {bookTitle[i]} | antal: {ammountOfBorrowedBooks[accountLoggedIn, i]}");
                }
            }
            pressEnterToReturn();
        }

        static int GetNumber(int answer, int minSize, int maxSize)
        {
            while (!int.TryParse(Console.ReadLine(), out answer) || answer < minSize || answer > maxSize)
            {
                
                Console.WriteLine($"svaret var inte ett heltal mellan {minSize} - {maxSize}");
                
            }

            return answer;
        }

    }
}
