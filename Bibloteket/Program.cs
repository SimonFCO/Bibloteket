//Simon Brink Sut25
//
//Problems
//Currently i need to remake so that the borrowing system is based on person instead of just one array for all of them
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
            if(answer == "A" || answer == "a")
            {
                tryLogin();
            }else if(answer == "B" || answer == "b")
            {

            }
            else
            {
                Console.WriteLine("Invalid, Try again");
                Console.ReadLine();
                startMenu();
            }
        }
        //This is the function for the login screen :D
        static void tryLogin()
        {
            
            Console.WriteLine("--- Login ---");
            Console.WriteLine("Username: ");
            string loginUsername = Console.ReadLine();
            int loginPin = 0;
            Console.WriteLine("Password: ");
            try
            {
                loginPin = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Input was not a valid integer.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The Input was either too large or too small");
            }

            //This should take care of the login but check it later if it works
            if(loginAttemps < 2)
            {
                loginAttemps += 1;

                for (int i = 0; i < usernames.Length; i++)
                {
                    if(loginUsername == usernames[i] && loginPin == pinCode[i]){
                      loggedIn = true;
                      Console.WriteLine($"Logged in as {loginUsername}");
                    }
                }

                if(loggedIn == false){
                  Console.WriteLine("Failed Login try again later");
                }
            }
            
        }
        
        //This is the navigationMenu Where after your login you can choose what to do!
        static void navigationMenu(){

          //1, Visa böcker
          //2, Låna Bok 
          //3, Lämna tillbaka Bok
          //4, Mina lån 
          //5. Logga ut
          
          Console.ReadLine();
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
          Console.WriteLine("Press enter when done");

        }
        //This code is clearly broken please fix one day
        //This will let the user borrow the books that he chooses
        static void borrowBooks(){
          Console.WriteLine("Vilken bok vill du låna? Skriv nummret av boken du vill låna");
          int answer = Convert.ToInt32(Console.ReadLine());
          if(aviableBooks[answer-1] != 0){
            aviableBooks[answer-1] -= 1;
            isBookBorrowed[answer-1] = true;
          }else{
            Console.WriteLine("You could not borrow that book, there might not be any left");
          }
          
        }

        static void returnBooks(){

        }

        static void myBorrowedBooks(){

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

        //Main Function
        static void Main(string[] args)
        {
            tryLogin();
            while(softwareIsAlive == true){
              navigationMenu();
            }
            
            
        }
    }
}
