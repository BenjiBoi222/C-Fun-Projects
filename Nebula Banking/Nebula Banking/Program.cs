namespace Nebula_Banking
{
    internal class Program
    {



        //Instance's to be able to reach non-static fields
        static LoginMenu loginMenu = new LoginMenu();
        static MainMenu mainMenu = new MainMenu();

        //Checks if a user logged in or out so it can clear the console
        public static bool switchFromMenu = false;

        //Static bools to determent if the user is logged in or not or that the menu should run
        public static bool isRunning = true;
        public static bool isLoggedIn = false;

        
        static void Main(string[] args)
        {

            while(isRunning)
            {
                if (switchFromMenu)
                {
                    Console.clear();
                    switchFromMenu = false;
                }


                if (isLoggedIn)
                {
                    mainMenu.ShowMenu();
                    int choice = UserChoice();
                    mainMenu.HandleMenu(choice);
                }
                else
                {
                    loginMenu.ShowMenu();
                    int choice = UserChoice();
                    loginMenu.HandleMenu(choice);
                }
            }
        }

        /// <summary>
        /// Helper function to the menus so no code repetition
        /// </summary>
        /// <returns>{int} - the option the user choose</returns>
        static int UserChoice()
        {
            Console.Write("Enter your options: ");
            if(int.TryParse(Console.ReadLine(), out int choice)) return choice;
            return -1;
        }
    }
}
