using System;

namespace battleShips
{
    internal class MainClass
    {
        public static int mapSize = 10;

        public static char[,] player1Map = new char[10, 10];
        public static char[,] player2Map = new char[10, 10];

        public static char[,] player1Attacks = new char[10, 10];
        public static char[,] player2Attacks = new char[10, 10];

        public static string player1Name = "player1";
        public static string player2Name = "player2";

        public static int playerTurn = 1;

        public static int gameMode = 1; // 1 = normal, 2 = Salvo

        private static void Main()
        {
            Menu();
        }

        // Display the menu
        public static void Menu()
        {
            Console.Clear();


            // ############################################################################################################
            // ASCII art of a battleship
            // ASCII art of a battleship
            // ASCII art of a battleship
            Console.WriteLine("                                        |__");
            Console.WriteLine("                                        |\\/");
            Console.WriteLine("                                        ---");
            Console.WriteLine("                                        / | [");
            Console.WriteLine("                                 !      | |||");
            Console.WriteLine("                               _/|     _/|-++'");
            Console.WriteLine("                           +  +--|    |--|--|_ |-");
            Console.WriteLine("                        { /|__|  |\\/__|  |--- |||__/");
            Console.WriteLine("                       +---------------___[}-_===_.'____                 /\\");
            Console.WriteLine("                   ____`-' ||___-{]_| _[}-  |     |_[___\\==--            \\/   _");
            Console.WriteLine(" __..._____--==/___]_|__|_____________________________[___\\==--____,------' .7");
            Console.WriteLine("|                                                                     BB-61/");
            Console.WriteLine(" \\_________________________________________________________________________|");
            Console.WriteLine("\n");
            // ASCII art of a battleship
            // Ascii art of a battleship
            // ASCII art of a battleship
            // ############################################################################################################


            Console.WriteLine("Welcome to Battleships!");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Tutorial");
            Console.WriteLine("3. Options");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice); // get the input
            } while (choice < 1 || choice > 4); //check if the input is valid

            switch (choice)
            {
                case 1: // Start the game
                    GameLoop();
                    break;
                case 2:
                    Show.Tutorial();
                    break;
                case 3:
                    Show.Options();
                    break;
                case 4: // Exit the game
                    Environment.Exit(0);
                    break;
            }
        }

        private static void GameLoop()
        {
            Initialize.Game();

            // Game loop
            switch (gameMode)
            {
                case 1: // Normal mode
                    while (true)
                    {
                        TurnBasedAttack(player1Name, player1Attacks, player2Map, player1Map);
                        TurnBasedAttack(player2Name, player2Attacks, player1Map, player2Map);
                    }
                case 2: // Salvo mode
                    Salvo.Loop();
                    break;
            }
        }

        private static void TurnBasedAttack(string playerName, char[,] playerAttacks, char[,] opponentMap,
            char[,] playerMap)
        {
            int row, col;
            do
            {
                if (!Check.ShipsLeft(opponentMap)) // check if there is a winner
                {
                    Console.Clear();
                    Console.WriteLine($"{playerName} wins! All ships of the opponent are sunk.");
                    Console.WriteLine("Press any key to return to menu.");
                    Console.ReadKey();
                    Menu();
                }

                Console.WriteLine($"{playerName}'s turn to attack!");
                Console.WriteLine("Your map:");
                Show.Board(playerMap);
                Console.WriteLine("Attack map:");
                Show.Board(playerAttacks);
                Console.Write("Enter row to attack (1-10): ");

                do
                {
                    if (int.TryParse(Console.ReadLine(), out row)) // get the row input
                        row -= 1; // 0-based index conversion
                } while (row < 0 || row >= mapSize); // check if the input is valid

                do
                {
                    Console.Write("Enter column to attack (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out col)) // get the column input
                        col -= 1; // 0-based index conversion
                } while (col < 0 || col >= mapSize); // check if the input is valid
            } while (Attack(row, col, opponentMap, playerAttacks));

            playerTurn = playerTurn == 1 ? 2 : 1; // Switch the player turn
            Console.Clear();
            Console.WriteLine("End of your turn. Press any button");
            Console.ReadKey();
        }

        // Let the player place ships manually on their board
        public static void PlaceShips(char[,] playerMap, string playerName)
        {
            string[] shipNames = { "carrier", "battleship", "destroyer", "submarine", "patrol boat" };
            int[] shipSizes = { 5, 4, 3, 3, 2 };

            for (var i = 0; i < shipNames.Length; i++) // Loop through the ships
            {
                Console.Clear();
                Show.Board(playerMap);

                Console.WriteLine($"{playerName}, place your ships on the board.");

                var validPlacement = false;
                while (validPlacement != true)
                {
                    Console.WriteLine($"Place your {shipNames[i]} (Size {shipSizes[i]})");
                    int row; // Variables to store the ship's starting position
                    do
                    {
                        Console.Write("Enter the row for the ship's starting position (1-10): ");
                        if (int.TryParse(Console.ReadLine(), out row)) row -= 1; // 0-based index conversion
                    } while (row < 0 || row > 9);

                    int col; // Variables to store the ship's starting position 
                    do
                    {
                        Console.Write("Enter the column for the ship's starting position (1-10): ");
                        if (int.TryParse(Console.ReadLine(), out col)) col -= 1; // 0-based index conversion
                    } while (col < 0 || col > 9);

                    int direction;
                    do
                    {
                        Console.Write("Enter direction (1 for horizontal, 2 for vertical): ");
                        int.TryParse(Console.ReadLine(), out direction); // get the direction input
                    } while (direction < 1 || direction > 2); // check if the input is valid

                    validPlacement =
                        Check.ShipPlacement(playerMap, row, col, shipSizes[i],
                            direction); // Check if the ship can be placed

                    if (validPlacement)
                    {
                        placeShipOnBoard(playerMap, row, col, shipSizes[i], direction, shipNames[i]);
                        Console.WriteLine($"{shipNames[i]} placed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid placement. Please try again.");
                    }
                }
            }
        }

        // Place the ship on the board
        public static void placeShipOnBoard(char[,] board, int row, int col, int size, int direction, string shipname)
        {
            if (direction == 1) // horizontal
                for (var i = 0; i < size; i++)
                    switch (shipname)
                    {
                        case "patrol boat":
                            board[row, col + i] = 'P'; // 'P' represents patrol boat
                            break;
                        case "submarine":
                            board[row, col + i] = 'S'; // 'S' represents submarine
                            break;
                        case "destroyer":
                            board[row, col + i] = 'D'; // 'D' represents destroyer
                            break;
                        case "battleship":
                            board[row, col + i] = 'B'; // 'B' represents battleship
                            break;
                        case "carrier":
                            board[row, col + i] = 'C'; // 'C' represents carrier
                            break;
                    }

            else // vertical
                for (var i = 0; i < size; i++)
                    switch (shipname)
                    {
                        case "patrol boat":
                            board[row + i, col] = 'P'; // 'P' represents patrol boat
                            break;
                        case "submarine":
                            board[row + i, col] = 'S'; // 'S' represents submarine
                            break;
                        case "destroyer":
                            board[row + i, col] = 'D'; // 'D' represents destroyer
                            break;
                        case "battleship":
                            board[row + i, col] = 'B'; // 'B' represents battleship
                            break;
                        case "carrier":
                            board[row + i, col] = 'C'; // 'C' represents carrier
                            break;
                    }
        }

        // Perform an attack on the opponent's map
        private static bool Attack(int row, int col, char[,] enemyPlayerMap, char[,] playerAttackMap)
        {
            // Check if there is a ship at the target location
            if (enemyPlayerMap[row, col] == 'S' || enemyPlayerMap[row, col] == 'D' || enemyPlayerMap[row, col] == 'B' ||
                enemyPlayerMap[row, col] == 'C' || enemyPlayerMap[row, col] == 'P')
            {
                var shipType = enemyPlayerMap[row, col];
                enemyPlayerMap[row, col] = 'X'; // Mark as hit
                playerAttackMap[row, col] = 'X';
                Console.WriteLine("Hit!");
                if (Check.ShipSunk(enemyPlayerMap, shipType)) Console.WriteLine("Ship sunk!");

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return true;
            }

            if (enemyPlayerMap[row, col] == '-') // Check if the target location is water
            {
                enemyPlayerMap[row, col] = 'O';
                playerAttackMap[row, col] = 'O';
                Console.WriteLine("Miss!");
                return false;
            }

            Console.WriteLine("Already tried!");
            return true;
        }
    }
}