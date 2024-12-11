using System;

namespace battleShips
{
    internal class Program
    {
        static char[,] player1Map = new char[10, 10];
        static char[,] player2Map = new char[10, 10];
        static char[,] player1Attacks = new char[10, 10];
        static char[,] player2Attacks = new char[10, 10];

        static int playerTurn = 1;

        static void Main(string[] args)
        {
            gameLoop();
        }

        public static void gameLoop()
        {
            initializeMaps();   // fill the maps with water
            Console.Clear();

            placeShips(player1Map); // Player 1 places their ships
            Console.Clear();
            placeShips(player2Map); // Player 2 places their ships
            Console.Clear();
            playerTurn = 1;
            while (true)
            {
                if (playerTurn == 1)
                {
                    int row, col;
                    do
                    {
                        Console.WriteLine($"player{playerTurn}'s Board:");
                        showBoard(player1Map);
                        Console.WriteLine("Attack map:");
                        showBoard(player1Attacks);
                        Console.WriteLine("Player 1's turn to attack!");
                        Console.Write("Enter row to attack (0-9): ");
                        do
                        {
                            int.TryParse(Console.ReadLine(), out row);
                        } while (row < 0 || row > 9);   //check if the input is valid
                        do
                        {
                            Console.Write("Enter column to attack (0-9): ");
                            int.TryParse(Console.ReadLine(), out col);
                        } while (col < 0 || col > 9);   //check if the input is valid
                    } while (attack(row, col));

                    if (!checkShipsLeft(player2Map)) // check if there is a winner
                    {
                        Console.Clear();
                        Console.WriteLine("Player 1 wins! All ships of Player 2 are sunk.");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("Next player. Press any button"); // switch to the next player
                    Console.ReadKey();
                    playerTurn = 2;
                }
                else
                {
                    int row, col;
                    do
                    {
                        Console.WriteLine($"player{playerTurn}'s Board:");
                        showBoard(player2Map);
                        Console.WriteLine("Attack map:");
                        showBoard(player2Attacks);
                        Console.WriteLine("Player 2's turn to attack!");
                        Console.Write("Enter row to attack (0-9): ");
                        do
                        {
                            int.TryParse(Console.ReadLine(), out row);
                        } while (row < 0 || row > 9);   //check if the input is valid
                        do
                        {
                            Console.Write("Enter column to attack (0-9): ");
                            int.TryParse(Console.ReadLine(), out col);
                        } while (col < 0 || col > 9);   //check if the input is valid
                    } while (attack(row, col));

                    if (!checkShipsLeft(player1Map)) // check if there is a winner
                    {
                        Console.Clear();
                        Console.WriteLine("Player 2 wins! All ships of Player 1 are sunk.");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("Next player. Press any button"); // switch to the next player
                    Console.ReadKey();
                    playerTurn = 1;
                }

                Console.Clear(); // Clear the screen for the next turn
            }
        }

        public static void initializeMaps()
        {
            // Fill the maps with water
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    player1Map[i, j] = '-';
                    player2Map[i, j] = '-';
                    player1Attacks[i, j] = '-';
                    player2Attacks[i, j] = '-';
                }
            }
        }

        public static void showBoard(char[,] board)
        {
            // Display the board
            Console.WriteLine("   0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("  ---------------------");
            for (int i = 0; i < 10; i++) // Loop through rows
            {
                Console.Write($"{i} |");
                for (int j = 0; j < 10; j++) // Loop through columns
                {
                    Console.Write($" {board[i, j]}"); // Display the cell
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Let the player place ships manually on their board
        public static void placeShips(char[,] board)
        {
            Console.WriteLine($"player {playerTurn}, place your ships on the board.");
            string[] shipNames = { "Carrier", "Battleship", "Destroyer", "Submarine", "Patrol Boat" };
            int[] shipSizes = { 5, 4, 3, 3, 2 };

            for (int i = 0; i < shipNames.Length; i++)
            {
                Console.Clear();
                showBoard(board);
                bool validPlacement = false;
                while (!validPlacement)
                {
                    Console.WriteLine($"Place your {shipNames[i]} (Size {shipSizes[i]})");
                    int row;
                    do
                    {
                        Console.Write("Enter the row for the ship's starting position (0-9): ");
                        int.TryParse(Console.ReadLine(), out row);
                    } while (row < 0 || row > 9);
                    int col;
                    do
                    {
                        Console.Write("Enter the column for the ship's starting position (0-9): ");
                        int.TryParse(Console.ReadLine(), out col);
                    } while (col < 0 || col > 9);

                    int direction;
                    do
                    {
                        Console.Write("Enter direction (0 for horizontal, 1 for vertical): ");
                        int.TryParse(Console.ReadLine(), out direction);
                    } while (direction < 0 || direction > 1);

                    validPlacement = checkShipPlacement(board, row, col, shipSizes[i], direction);

                    if (validPlacement)
                    {
                        placeShipOnBoard(board, row, col, shipSizes[i], direction);
                        Console.WriteLine($"{shipNames[i]} placed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid placement. Please try again.");
                    }
                }
            }
            playerTurn = 2;
            Console.Clear();
            Console.WriteLine("Next player. Press any button");
            Console.ReadKey();
        }

        // Check if the ship can be placed in the given position and direction
        public static bool checkShipPlacement(char[,] board, int row, int col, int size, int direction)
        {
            if (direction == 0) // horizontal
            {
                if (col + size > 10) return false;
                for (int i = 0; i < size; i++)
                {
                    if (board[row, col + i] != '-') return false; // check if already occupied
                }
            }
            else // vertical
            {
                if (row + size > 10) return false;
                for (int i = 0; i < size; i++)
                {
                    if (board[row + i, col] != '-') return false; // check if already occupied
                }
            }
            return true;
        }

        // Place the ship on the board
        public static void placeShipOnBoard(char[,] board, int row, int col, int size, int direction)
        {
            if (direction == 0) // horizontal
            {
                for (int i = 0; i < size; i++)
                {
                    board[row, col + i] = 'S'; // 'S' represents ship
                }
            }
            else // vertical
            {
                for (int i = 0; i < size; i++)
                {
                    board[row + i, col] = 'S'; // 'S' represents ship
                }
            }
        }

        // Perform an attack on the opponent's map
        public static bool attack(int row, int col)
        {
            if (playerTurn == 1)
            {
                if (player2Map[row, col] == 'S')
                {
                    player2Map[row, col] = 'X'; // Mark as hit
                    player1Attacks[row, col] = 'X';
                    Console.WriteLine("Hit!");
                    return true;
                }
                else if (player2Map[row, col] != 'X')
                {
                    player2Map[row, col] = 'O';
                    player1Attacks[row, col] = 'O';
                    Console.WriteLine("Miss!");
                    return false;
                }
                else
                {
                    Console.WriteLine("Already tried!");
                    return true;
                }
            }
            else
            {
                if (player1Map[row, col] == 'S')
                {
                    player1Map[row, col] = 'X'; // Mark as hit
                    player2Attacks[row, col] = 'X';
                    Console.WriteLine("Hit!");
                    return true;
                }
                else if (player1Map[row, col] != 'X')
                {
                    player1Map[row, col] = 'O';
                    player2Attacks[row, col] = 'O';
                    Console.WriteLine("Miss!");
                    return false;
                }
                else
                {
                    Console.WriteLine("Already tried!");
                    return true;
                }

            }
        }

        // Check if all ships have been sunk
        public static bool checkShipsLeft(char[,] board)
        {
            foreach (var cell in board)
            {
                if (cell == 'S')
                {
                    return true; // There are still ships on the board
                }
            }
            return false; // No ships left
        }

    }
}