using System;

namespace battleShips
{
    internal class Program
    {
        private static int _mapSize = 10;

        private static char[,] _player1Map = new char[10, 10];
        private static char[,] _player2Map = new char[10, 10];

        private static char[,] _player1Attacks = new char[10, 10];
        private static char[,] _player2Attacks = new char[10, 10];

        private static string _player1Name = "player1";
        private static string _player2Name = "player2";

        private static int _playerTurn = 1;

        private static void Main()
        {
            Menu();
        }

        // Display the menu
        private static void Menu()
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
                    ShowTutorial();
                    break;
                case 3:
                    ShowOptions();
                    break;
                case 4: // Exit the game
                    Environment.Exit(0);
                    break;
            }
        }

        private static void GameLoop()
        {
            InitializeGame();

            // Game loop
            while (true)
                if (_playerTurn == 1)
                    TurnBasedAttack(_player1Name, _player1Attacks, _player2Map, _player1Map);
                else
                    TurnBasedAttack(_player2Name, _player2Attacks, _player1Map, _player2Map);
        }

        private static void ShowTutorial()
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
            Menu();
        }

        private static void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("Options:");
            Console.WriteLine("1. Change board size");
            //TODO: add a gamemode switch option
            Console.WriteLine("2. Return to menu");
            Console.Write("Enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice); // get the input
            } while (choice < 1 || choice > 2); //check if the input is valid


            switch (choice)
            {
                case 1:
                    ChangeBoardSize();
                    break;
                case 2:
                    Menu();
                    break;
            }
        }

        private static void ChangeBoardSize()
        {
            Console.Clear();
            Console.WriteLine("Enter the size of the board (default: 10): ");
            do
            {
                int.TryParse(Console.ReadLine(), out _mapSize); // get the input
            } while (_mapSize < 1); // check if the input is valid

            // Initialize the maps with the new size
            _player1Map = new char[_mapSize, _mapSize];
            _player2Map = new char[_mapSize, _mapSize];
            _player1Attacks = new char[_mapSize, _mapSize];
            _player2Attacks = new char[_mapSize, _mapSize];

            Menu();
        }


        private static void SetPlayerName()
        {
            var userInput = Console.ReadLine();

            if (_playerTurn == 1 && userInput != "")
            {
                _player1Name = userInput;
                _playerTurn = 2;
            }
            else if (_playerTurn == 2 && userInput != "")
            {
                _player2Name = userInput;
                _playerTurn = 1;
            }
        }

        private static void
            InitializeShips(char[,] playerMap, string playerName) // place, and replace starting positions of the ships
        {
            PlaceShips(playerMap, playerName); // Player 1 places their ships
            Console.Clear();

            do
            {
                Console.WriteLine("You have the option to replace your ships.");
                Console.WriteLine("enter 'exit' if you want to skip, or enter the character representing your ship");
                ShowBoard(playerMap);
                var userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    break;
                }
                else if (userInput == "S" || userInput == "D" || userInput == "B" || userInput == "C" ||
                         userInput == "P")
                {
                    ReplaceShip(playerMap, userInput);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while (true);

            Console.Clear();
            Console.WriteLine("Next player. Press any button");
        }

        private static void InitializeGame()
        {
            Console.Clear();
            InitializeMaps();
            Console.WriteLine("Player 1, choose your name. default:player1");
            SetPlayerName();

            Console.Clear();
            Console.WriteLine("Player 2, choose your name. default:player2");
            SetPlayerName();

            Console.Clear();
            InitializeShips(_player1Map, _player1Name);
            InitializeShips(_player2Map, _player2Name);

            Console.Clear();
        }

        private static void TurnBasedAttack(string playerName, char[,] playerAttacks, char[,] opponentMap,
            char[,] playerMap)
        {
            int row, col;
            do
            {
                if (!CheckShipsLeft(opponentMap)) // check if there is a winner
                {
                    Console.Clear();
                    Console.WriteLine($"{playerName} wins! All ships of the opponent are sunk.");
                    Console.WriteLine("Press any key to return to menu.");
                    Console.ReadKey();
                    Menu();
                }

                Console.WriteLine($"{playerName}'s turn to attack!");
                Console.WriteLine("Your map:");
                ShowBoard(playerMap);
                Console.WriteLine("Attack map:");
                ShowBoard(playerAttacks);
                Console.Write("Enter row to attack (1-10): ");

                do
                {
                    if (int.TryParse(Console.ReadLine(), out row)) // get the row input
                        row -= 1; // 0-based index conversion
                } while (row < 0 || row >= _mapSize); // check if the input is valid

                do
                {
                    Console.Write("Enter column to attack (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out col)) // get the column input
                        col -= 1; // 0-based index conversion
                } while (col < 0 || col >= _mapSize); // check if the input is valid
            } while (Attack(row, col, opponentMap, playerAttacks));

            _playerTurn = _playerTurn == 1 ? 2 : 1; // Switch the player turn
            Console.Clear();
            Console.WriteLine("End of your turn. Press any button");
            Console.ReadKey();
        }

        private static void InitializeMaps()
        {
            // Fill the maps with water
            for (var i = 0; i < _mapSize; i++)
            for (var j = 0; j < _mapSize; j++)
            {
                // '-' represents water
                _player1Map[i, j] = '-';
                _player2Map[i, j] = '-';
                _player1Attacks[i, j] = '-';
                _player2Attacks[i, j] = '-';
            }
        }

        private static void ShowBoard(char[,] board)
        {
            Console.Write("    "); // Add indentation for alignment

            // Dynamic board numbers
            for (var i = 0; i < _mapSize; i++)
                Console.Write(i >= 9 ? $" {i + 1}" : $" {i + 1} "); // Display the column number

            Console.WriteLine();

            Console.Write("    "); // Add indentation for alignment
            for (var i = 0; i < _mapSize; i++) Console.Write("═══"); // Dynamic horizontal lines

            Console.WriteLine();

            for (var i = 0; i < _mapSize; i++)
            {
                // Add an extra space for double-digit row numbers
                Console.Write(i >= 9 ? $"{i + 1} ║" : $"{i + 1}  ║"); // Display the row number

                for (var j = 0; j < _mapSize; j++) // Loop through columns
                    Console.Write($" {board[i, j]} "); // Display the cell

                Console.WriteLine();
            }

            Console.WriteLine();
        }


        // Let the player place ships manually on their board
        private static void PlaceShips(char[,] playerMap, string playerName)
        {
            string[] shipNames = { "carrier", "battleship", "destroyer", "submarine", "patrol boat" };
            int[] shipSizes = { 5, 4, 3, 3, 2 };

            for (var i = 0; i < shipNames.Length; i++) // Loop through the ships
            {
                Console.Clear();
                ShowBoard(playerMap);

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
                        CheckShipPlacement(playerMap, row, col, shipSizes[i],
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

        // Check if the ship can be placed in the given position and direction
        private static bool CheckShipPlacement(char[,] board, int row, int col, int size, int direction)
        {
            if (direction == 1) // horizontal
            {
                if (col + size > 10) return false;
                for (var i = 0; i < size; i++)
                    if (board[row, col + i] != '-')
                        return false; // check if already occupied
            }
            else // vertical
            {
                if (row + size > 10) return false;
                for (var i = 0; i < size; i++)
                    if (board[row + i, col] != '-')
                        return false; // check if already occupied
            }

            return true;
        }

        // Place the ship on the board
        private static void placeShipOnBoard(char[,] board, int row, int col, int size, int direction, string shipname)
        {
            if (direction == 1) // horizontal
            {
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
            }

            else // vertical
            {
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
        }

        // Perform an attack on the opponent's map
        //TODO: refactor this method due to duplication
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
                if (CheckShipSunk(enemyPlayerMap, shipType)) Console.WriteLine("Ship sunk!");

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return true;
            }
            else if (enemyPlayerMap[row, col] == '-') // Check if the target location is water
            {
                enemyPlayerMap[row, col] = 'O';
                playerAttackMap[row, col] = 'O';
                Console.WriteLine("Miss!");
                return false;
            }
            else
            {
                Console.WriteLine("Already tried!");
                return true;
            }
        }

        // Check if a ship has been sunk
        private static bool CheckShipSunk(char[,] board, char shipType)
        {
            foreach (var cell in board)
                if (cell == shipType)
                    return false; // The ship has not been sunk

            return true; // The ship has been sunk
        }

        // Check if all ships have been sunk
        private static bool CheckShipsLeft(char[,] board)
        {
            foreach (var cell in board)
                if (cell == 'S' || cell == 'D' || cell == 'B' || cell == 'C' || cell == 'P')
                    return true; // There are still ships on the board

            return false; // No ships left
        }

        private static void ReplaceShip(char[,] map, string ship)
        {
            var sizeCounter = PickUpShip(map, ship); // Pick up the ship
            Console.Clear();
            Console.WriteLine("Ship successfully picked up!");
            ShowBoard(map);
            Console.WriteLine($"Place your size :{sizeCounter} ship.");

            int row; // Variables to store the ship's starting position
            do
            {
                Console.Write("Enter the row for the ship's starting position (1-10): ");
                if (int.TryParse(Console.ReadLine(), out row))
                {
                    row -= 1; // Subtract 1 to convert to 0-based index
                }
            } while (row < 0 || row > 9);

            int col; // Variables to store the ship's starting position
            do
            {
                Console.Write("Enter the column for the ship's starting position (1-10): ");
                if (int.TryParse(Console.ReadLine(), out col))
                {
                    col -= 1; // Subtract 1 to convert to 0-based index
                }
            } while (col < 0 || col > 9);

            int direction;
            do
            {
                Console.Write("Enter direction (1 for horizontal, 2 for vertical): ");
                int.TryParse(Console.ReadLine(), out direction); // get the direction input
            } while (direction < 1 || direction > 2); // check if the input is valid

            var validPlacement =
                CheckShipPlacement(map, row, col, sizeCounter, direction); // Check if the ship can be placed

            if (validPlacement)
            {
                placeShipOnBoard(map, row, col, sizeCounter, direction, ship); // Place the ship on the board
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
            for (var i = 0; i < _mapSize; i++)
            for (var j = 0; j < _mapSize; j++)
                if (map[i, j] == Convert.ToChar(ship))
                {
                    map[i, j] = '-';
                    sizeCounter++;
                }

            return sizeCounter;
        }
    }
}