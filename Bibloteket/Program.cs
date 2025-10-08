using System.Globalization;

namespace Bibloteket
{
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

            }
            catch (OverflowException)
            {

            }

            

            if (loginUsername == usernames[0] && loginPin == pinCode[0])
            {
                loggedIn = true;
                Console.WriteLine($"Logged in as {loginUsername}");
            }
            else
            {
                Console.WriteLine("Failed Login try again");
                tryLogin();
            }
        }

        //These are where most of the variables are stored
        
        static string[] usernames = {"Bengt","Stig","Rutger","Sten", "Harald"};
        static int[] pinCode = {123456, 234567, 345678, 987654, 876554 };
        static bool loggedIn;

        //Main Function
        static void Main(string[] args)
        {

            tryLogin();
            

        }
    }
}
