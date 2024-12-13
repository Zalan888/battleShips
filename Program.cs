using System;

namespace battleShips
{
	internal class Program
	{
		private static char[,] _player1Map = new char[10, 10]; // Player 1's board
		private static char[,] _player2Map = new char[10, 10]; // Player 2's board
		private static char[,] _player1Attacks = new char[10, 10]; // Player 1's attack map on Player 2 
		private static char[,] _player2Attacks = new char[10, 10]; // Player 2's attack map on Player 1
		
		//TODO: change the names to be dynamic
		private static string _player1Name; // Name chosen by player1
		private static string _player2Name; // Name chosen by player2
		
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
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice); // get the input
            } while (choice < 1 || choice > 3); //check if the input is valid

            // switch to the selected option
            switch (choice)
            {
                case 1: // Start the game
                    GameLoop();
                    break;
                case 2:  // Display the tutorial
                    Tutorial();	
                    break;
                case 3: // Exit the game
                    Environment.Exit(0);
                    break;
            }
		}

		// Display the tutorial
		private static void Tutorial()
		{
			Console.Clear();
			Console.WriteLine("Welcome to Battleships!");
			Console.WriteLine("The game is played on four 10x10 grids, two for each player.");
			Console.WriteLine("The grids are typically square – usually 10×10 – and the individual squares in the grid are identified by numbers.");
			Console.WriteLine("On one grid the player arranges ships and records the shots by the opponent.");
			Console.WriteLine("On the other grid the player records their own shots.");
			Console.WriteLine("Before play begins, each player secretly arranges their ships on their primary grid.");
			Console.WriteLine("Each ship occupies a number of consecutive squares on the grid, arranged either horizontally or vertically.");
			Console.WriteLine("The number of squares for each ship is determined by the type of the ship.");
			Console.WriteLine("The ships cannot overlap (i.e., only one ship can occupy any given square in the grid).");
			Console.WriteLine("The types and numbers of ships allowed are the same for each player.");
			Console.WriteLine("After the ships have been positioned, the game proceeds in a series of rounds.");
			Console.WriteLine("In each round, each player takes a turn to announce a target square in the opponent's grid which is to be shot at.");
			Console.WriteLine("Press any key to return to the menu!");
			Console.ReadKey();
			Console.Clear();
			Menu();
		}
		
		private static void SetPlayerNames()
		{
			if (_playerTurn == 1)
			{
				_player1Name = Console.ReadLine();
				if (_player1Name == null || _player1Name.Length == 0)
				{
					_player1Name = "Player1"; // set a default value
				}

				_playerTurn = 2; // assigning 2 for playerturn, as the function is based on this value
			}
			else
			{
				_player2Name = Console.ReadLine();
				if (_player2Name == null || _player2Name.Length == 0)
				{
					_player2Name = "Player2"; // set a default value
				}

				_playerTurn = 1; // assigning 1 for playerturn, as the function is based on this value
			}
		}

		// initialize the game's loop
		private static void GameLoop()
		{
			InitializeMaps();   // fill the maps with water
			Console.Clear();

			Console.WriteLine("Player 1, choose your name. default:player1");
			SetPlayerNames();
			Console.Clear();
			Console.WriteLine("Player 2, choose your name. default:player2");
			SetPlayerNames();
			Console.Clear();
			
			PlaceShips(_player1Map); // Player 1 places their ships
			Console.Clear();
			
			
			PlaceShips(_player2Map); // Player 2 places their ships
			Console.Clear();
			_playerTurn = 1;
			
			// Game loop:
			while (true)
			{
				if (_playerTurn == 1) // Player 1's turn
				{
					int row, col; // Variables to store the attack coordinates
					do
					{
						// Display the boards and ask for the row and column to attack
						Console.WriteLine($"{_player1Name}'s Board:");	
						ShowBoard(_player1Map);
						Console.WriteLine("Attack map:");
						ShowBoard(_player1Attacks);
						Console.WriteLine($"{_player1Name}'s turn to attack!");
						Console.Write("Enter row to attack (0-9): "); 
						// display the boards and ask for the row and column to attack
						
						do
						{
							if (int.TryParse(Console.ReadLine(), out row)) // get the row input
							{
								row -= 1; // Subtract 1 to convert to 0-based index
							}
						} while (row < 0 || row > 9);   // check if the input is valid

						do
						{
							Console.Write("Enter column to attack (0-9): ");
							if (int.TryParse(Console.ReadLine(), out col)) // get the column input
                            {
                                col -= 1; // Subtract 1 to convert to 0-based index
                            }
						} while (col < 0 || col > 9);   // check if the input is valid
					} while (Attack(row, col));

					if (!CheckShipsLeft(_player2Map)) // check if there is a winner
					{
						Console.Clear();
						Console.WriteLine($" {_player1Name} wins! All ships of{_player2Name} are sunk.");
						break;
					}

					Console.Clear();
					Console.WriteLine("End of your turn. Press any button");
					Console.ReadKey();
					_playerTurn = 2; // switch to the next player
				}
				else // Player 2's turn
				{
					int row, col; // Variables to store the attack coordinates
					do
					{
						// Display the boards and ask for the row and column to attack
						Console.WriteLine($"{_player2Name}'s Board:");
						ShowBoard(_player2Map);
						Console.WriteLine("Attack map:");
						ShowBoard(_player2Attacks);
						Console.WriteLine($" {_player2Name}'s turn to attack!");
						Console.Write("Enter row to attack (0-9): ");
						// display the boards and ask for the row and column to attack
						
						do
						{
							int.TryParse(Console.ReadLine(), out row); // get the row input
						} while (row < 0 || row > 9);   // check if the input is valid
						do
						
						{
							Console.Write("Enter column to attack (0-9): ");
							int.TryParse(Console.ReadLine(), out col); //	get the column input
						} while (col < 0 || col > 9);   // check if the input is valid
					} while (Attack(row, col));

					if (!CheckShipsLeft(_player1Map)) // check if there is a winner
					{
						Console.Clear();
						Console.WriteLine($"{_player2Name} wins! All ships of {_player1Name} are sunk.");
						break;
					}

					Console.Clear();
					Console.WriteLine("End of your turn. Press any button");
					Console.ReadKey();
					_playerTurn = 1; // switch to the next player
				}

				Console.Clear(); // Clear the screen for the next turn
			}
		}

		private static void InitializeMaps()
		{
			// Fill the maps with water
			for (int i = 0; i < 10; i++) // Loop through rows
			{
				for (int j = 0; j < 10; j++) // Loop through columns
				{
					// '-' represents water
					_player1Map[i, j] = '-'; 
					_player2Map[i, j] = '-';
					_player1Attacks[i, j] = '-';
					_player2Attacks[i, j] = '-';
					// '-' represents water
				}
			}
		}

		private static void ShowBoard(char[,] board)
		{
			// Display the board
			Console.WriteLine("   1 2 3 4 5 6 7 8 9 10"); // Display the column numbers
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
		private static void PlaceShips(char[,] board)
		{
			string[] shipNames = { "Carrier", "Battleship", "Destroyer", "Submarine", "Patrol Boat" }; // Names of the ships
			int[] shipSizes = { 5, 4, 3, 3, 2 }; // Sizes of the ships

			for (int i = 0; i < shipNames.Length; i++) // Loop through the ships
			{
				Console.Clear();
				ShowBoard(board); // Display the board
				
				//display the player's name
				// ############################################################################################################
				if (_playerTurn == 1)  
				{
					Console.WriteLine($"{_player1Name}, place your ships on the board.");
				}
				else
				{
					Console.WriteLine($"{_player2Name}, place your ships on the board.");
				}
				// ############################################################################################################
				//display the player's name
				
				bool validPlacement = false; // Flag to check if the ship can be placed
				while (!validPlacement) // Loop until the ship is placed correctly
				{
					Console.WriteLine($"Place your {shipNames[i]} (Size {shipSizes[i]})");
					int row; // Variables to store the ship's starting position
					do
					{
						Console.Write("Enter the row for the ship's starting position (1-10): ");
						if (int.TryParse(Console.ReadLine(), out row ))
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
						int.TryParse(Console.ReadLine(), out direction);
					} while (direction < 1 || direction > 2);

					validPlacement = CheckShipPlacement(board, row, col, shipSizes[i], direction);

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
			_playerTurn = 2;
			Console.Clear();
			Console.WriteLine("Next player. Press any button");
			Console.ReadKey();
		}

		// Check if the ship can be placed in the given position and direction
		private static bool CheckShipPlacement(char[,] board, int row, int col, int size, int direction)
		{
			if (direction == 1) // horizontal
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
		private static void placeShipOnBoard(char[,] board, int row, int col, int size, int direction)
		{
			if (direction == 1) // horizontal
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
		private static bool Attack(int row, int col)
		{
			if (_playerTurn == 1)
			{
				if (_player2Map[row, col] == 'S')
				{
					_player2Map[row, col] = 'X'; // Mark as hit
					_player1Attacks[row, col] = 'X';
					Console.WriteLine("Hit!");
					return true;
				}
				else if (_player2Map[row, col] != 'X')
				{
					_player2Map[row, col] = 'O';
					_player1Attacks[row, col] = 'O';
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
				if (_player1Map[row, col] == 'S')
				{
					_player1Map[row, col] = 'X'; // Mark as hit
					_player2Attacks[row, col] = 'X';
					Console.WriteLine("Hit!");
					return true;
				}
				else if (_player1Map[row, col] != 'X')
				{
					_player1Map[row, col] = 'O';
					_player2Attacks[row, col] = 'O';
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
		private static bool CheckShipsLeft(char[,] board)
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