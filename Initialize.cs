using System;

namespace battleShips
{
    internal class Initialize
    {
        public static void Game()
        {
            Console.Clear();
            Maps();
            Console.WriteLine("Player 1, choose your name. default:player1");
            PlayerNames();

            Console.Clear();
            Console.WriteLine("Player 2, choose your name. default:player2");
            PlayerNames();

            Console.Clear();
            Ships(MainClass.player1Map, MainClass.player1Name);
            Ships(MainClass.player2Map, MainClass.player2Name);

            Console.Clear();
        }

        private static void PlayerNames()
        {
            var userInput = Console.ReadLine();

            if (MainClass.playerTurn == 1 && userInput != "")
            {
                MainClass.player1Name = userInput;
                MainClass.playerTurn = 2;
            }
            else if (MainClass.playerTurn == 2 && userInput != "")
            {
                MainClass.player2Name = userInput;
                MainClass.playerTurn = 1;
            }
        }

        private static void
            Ships(char[,] playerMap, string playerName) // place, and replace starting positions of the ships
        {
            MainClass.PlaceShips(playerMap, playerName); // Player 1 places their ships
            Console.Clear();

            do
            {
                Console.WriteLine("You have the option to replace your ships.");
                Console.WriteLine("enter 'exit' if you want to skip, or enter the character representing your ship");
                Show.Board(playerMap);
                var userInput = Console.ReadLine();
                if (userInput == "exit") break;

                if (userInput == "S" || userInput == "D" || userInput == "B" || userInput == "C" ||
                    userInput == "P")
                    ReplaceShip(playerMap, userInput);
                else
                    Console.WriteLine("Invalid input. Please try again.");
            } while (true);

            Console.Clear();
            Console.WriteLine("Next player. Press any button");
        }

        private static void Maps()
        {
            // Fill the maps with water
            for (var i = 0; i < MainClass.mapSize; i++)
            for (var j = 0; j < MainClass.mapSize; j++)
            {
                // '-' represents water
                MainClass.player1Map[i, j] = '-';
                MainClass.player2Map[i, j] = '-';
                MainClass.player1Attacks[i, j] = '-';
                MainClass.player2Attacks[i, j] = '-';
            }
        }

        private static void ReplaceShip(char[,] map, string ship)
        {
            var sizeCounter = PickUpShip(map, ship); // Pick up the ship
            Console.Clear();
            Console.WriteLine("Ship successfully picked up!");
            Show.Board(map);
            Console.WriteLine($"Place your size :{sizeCounter} ship.");

            int row; // Variables to store the ship's starting position
            do
            {
                Console.Write("Enter the row for the ship's starting position (1-10): ");
                if (int.TryParse(Console.ReadLine(), out row)) row -= 1; // Subtract 1 to convert to 0-based index
            } while (row < 0 || row > 9);

            int col; // Variables to store the ship's starting position
            do
            {
                Console.Write("Enter the column for the ship's starting position (1-10): ");
                if (int.TryParse(Console.ReadLine(), out col)) col -= 1; // Subtract 1 to convert to 0-based index
            } while (col < 0 || col > 9);

            int direction;
            do
            {
                Console.Write("Enter direction (1 for horizontal, 2 for vertical): ");
                int.TryParse(Console.ReadLine(), out direction); // get the direction input
            } while (direction < 1 || direction > 2); // check if the input is valid

            var validPlacement =
                Check.ShipPlacement(map, row, col, sizeCounter, direction); // Check if the ship can be placed

            if (validPlacement)
            {
                MainClass.placeShipOnBoard(map, row, col, sizeCounter, direction, ship); // Place the ship on the board
                Console.WriteLine($"{ship} placed successfully!");
            }
            else
            {
                Console.WriteLine("Invalid placement. Please try again.");
            }
        }

        private static int PickUpShip(char[,] map, string ship)
        {
            var sizeCounter = 0;
            for (var i = 0; i < MainClass.mapSize; i++)
            for (var j = 0; j < MainClass.mapSize; j++)
                if (map[i, j] == Convert.ToChar(ship))
                {
                    map[i, j] = '-';
                    sizeCounter++;
                }

            return sizeCounter;
        }
    }
}