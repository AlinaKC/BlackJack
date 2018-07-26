using System;

namespace BlackJack
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("A Game of BlackJack");
            Console.WriteLine("===================");

            bool isInvalid = true;
            while (isInvalid)
            {
                Console.Write("What would you like to do: deal, quit?: ");
                string response = Console.ReadLine();
                switch (response.ToUpper())
                {
                    case "DEAL":
                        Game game = new Game();
                        game.DealInitialCards();
                        isInvalid = false;
                        break;
                    case "QUIT":
                        Environment.Exit(-1);
                        isInvalid = false;
                        break;
                    default:
                        Console.WriteLine("Wrong Command. Try again");
                        break;
                }
            }

        }
    }
}
