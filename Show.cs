using System;

namespace battleShips
{
    public class Show
    {
        public static void Tutorial()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Battleships!");
            Console.WriteLine("The game is played on four 10x10 grids, two for each player.");
            Console.WriteLine(
                "The grids are typically square – usually 10×10 – and the individual squares in the grid are identified by numbers.");
            Console.WriteLine("On one grid the player arranges ships and records the shots fired by the opponent.");
            Console.WriteLine("On the other grid the player records their own shots.");
            Console.WriteLine("Before play begins, each player secretly arranges their ships on their primary grid.");
            Console.WriteLine(
                "Each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically.");
            Console.WriteLine("The number of squares for each ship is determined by the type of the ship.");
            Console.WriteLine(
                "The ships cannot overlap (i.e., only one ship can occupy any given square in the grid).");
            Console.WriteLine("The types and numbers of ships allowed are the same for each player.");
            Console.WriteLine("After the ships have been positioned, the game proceeds in a series of rounds.");
            Console.WriteLine(
                "In each round, each player takes a turn to announce a target square in the opponent's grid which is to be shot at.");
            Console.WriteLine("Press any key to return to the menu!");
            Console.ReadKey();
            Console.Clear();
            MainClass.Menu();
        }

        public static void Options()
        {
            Console.Clear();
            Console.WriteLine("Options:");
            Console.WriteLine("1. Change board size");
            //TODO: add a gamemode switch option
            Console.WriteLine("2. Gamemode: ");
            Console.WriteLine("3. Return to menu");
            Console.Write("Enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice); // get the input
            } while (choice < 1 || choice > 3); //check if the input is valid


            switch (choice)
            {
                case 1:
                    battleShips.Options.ChangeBoardSize();
                    break;
                case 2:
                    battleShips.Options.ChangeGamemode();
                    break;
                case 3:
                    MainClass.Menu();
                    break;
            }
        }

        public static void Board(char[,] board)
        {
            Console.Write("    "); // Add indentation for alignment

            // Dynamic board numbers
            for (var i = 0; i < MainClass.mapSize; i++)
                Console.Write(i >= 9 ? $" {i + 1}" : $" {i + 1} "); // Display the column number

            Console.WriteLine();

            Console.Write("    "); // Add indentation for alignment
            for (var i = 0; i < MainClass.mapSize; i++) Console.Write("═══"); // Dynamic horizontal lines

            Console.WriteLine();

            for (var i = 0; i < MainClass.mapSize; i++)
            {
                // Add an extra space for double-digit row numbers
                Console.Write(i >= 9 ? $"{i + 1} ║" : $"{i + 1}  ║"); // Display the row number

                for (var j = 0; j < MainClass.mapSize; j++) // Loop through columns
                    Console.Write($" {board[i, j]} "); // Display the cell

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}