using System;

namespace battleShips
{
    public class Options
    {
        public static void ChangeBoardSize()
        {
            Console.Clear();
            Console.WriteLine("Enter the size of the board (default: 10): ");
            do
            {
                int.TryParse(Console.ReadLine(), out MainClass.mapSize); // get the input
            } while (MainClass.mapSize < 1); // check if the input is valid

            // Initialize the maps with the new size
            MainClass.player1Map = new char[MainClass.mapSize, MainClass.mapSize];
            MainClass.player2Map = new char[MainClass.mapSize, MainClass.mapSize];
            MainClass.player1Attacks = new char[MainClass.mapSize, MainClass.mapSize];
            MainClass.player2Attacks = new char[MainClass.mapSize, MainClass.mapSize];

            MainClass.Menu();
        }

        public static void ChangeGamemode()
        {
            Console.Clear();
            Console.WriteLine("Available gamemodes:");
            Console.WriteLine("1. normal (5 ships, 1 shot per turn + 1 shot per hit)");
            Console.WriteLine("2. Salvo' ship (5 ships, amount of shots = amount of ship zones / 3)");
            Console.WriteLine("Please enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice); // get the input
            } while (choice < 1 || choice > 2); // check if the input is valid

            switch (choice)
            {
                case 1:
                    MainClass.gameMode = 1; // normal
                    break;
                case 2:
                    MainClass.gameMode = 2; // Salvo
                    break;
            }

            MainClass.Menu();
        }
    }
}