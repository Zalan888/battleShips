using System;

namespace battleShips
{
	internal class Program
	{
		static char[,] _player1Map = new char[10, 10]; // Player 1's board
		static char[,] _player2Map = new char[10, 10]; // Player 2's board
		static char[,] _player1Attacks = new char[10, 10]; // Player 1's attack map on Player 2 
		static char[,] _player2Attacks = new char[10, 10]; // Player 2's attack map on Player 1
		
		// TODO: implement custom player names
		static int _playerTurn = 1;

		static void Main()
		{
			Menu();
		}

		private static void Menu()
		{
			Console.Clear(); 
			
			
			
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

			

			Console.WriteLine("Welcome to Battleships!");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Tutorial");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > 3);

            switch (choice)
            {
                case 1:
                    GameLoop();
                    break;
                case 2: 
                    Tutorial();	
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
		}

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

		private static void GameLoop()
		{
			InitializeMaps();   // fill the maps with water
			Console.Clear();

			PlaceShips(_player1Map); // Player 1 places their ships
			Console.Clear();
			PlaceShips(_player2Map); // Player 2 places their ships
			Console.Clear();
			_playerTurn = 1;
			while (true)
			{
				if (_playerTurn == 1)
				{
					int row, col;
					do
					{
						Console.WriteLine($"player{_playerTurn}'s Board:");	// Display the board
						ShowBoard(_player1Map);
						Console.WriteLine("Attack map:");
						ShowBoard(_player1Attacks);
						Console.WriteLine("Player 1's turn to attack!");
						Console.Write("Enter row to attack (0-9): "); 
						do
						{
							int.TryParse(Console.ReadLine(), out row); // get the row input
						} while (row < 0 || row > 9);   //check if the input is valid
						do
						{
							Console.Write("Enter column to attack (0-9): ");
							int.TryParse(Console.ReadLine(), out col);
						} while (col < 0 || col > 9);   //check if the input is valid
					} while (Attack(row, col));

					if (!CheckShipsLeft(_player2Map)) // check if there is a winner
					{
						Console.Clear();
						Console.WriteLine("Player 1 wins! All ships of Player 2 are sunk.");
						break;
					}

					Console.Clear();
					Console.WriteLine("Next player. Press any button"); // switch to the next player
					Console.ReadKey();
					_playerTurn = 2;
				}
				else
				{
					int row, col;
					do
					{
						Console.WriteLine($"player{_playerTurn}'s Board:");
						ShowBoard(_player2Map);
						Console.WriteLine("Attack map:");
						ShowBoard(_player2Attacks);
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
					} while (Attack(row, col));

					if (!CheckShipsLeft(_player1Map)) // check if there is a winner
					{
						Console.Clear();
						Console.WriteLine("Player 2 wins! All ships of Player 1 are sunk.");
						break;
					}

					Console.Clear();
					Console.WriteLine("Next player. Press any button"); // switch to the next player
					Console.ReadKey();
					_playerTurn = 1;
				}

				Console.Clear(); // Clear the screen for the next turn
			}
		}

		private static void InitializeMaps()
		{
			// Fill the maps with water
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					_player1Map[i, j] = '-';
					_player2Map[i, j] = '-';
					_player1Attacks[i, j] = '-';
					_player2Attacks[i, j] = '-';
				}
			}
		}

		private static void ShowBoard(char[,] board)
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
		private static void PlaceShips(char[,] board)
		{
			Console.WriteLine($"player {_playerTurn}, place your ships on the board.");
			string[] shipNames = { "Carrier", "Battleship", "Destroyer", "Submarine", "Patrol Boat" };
			int[] shipSizes = { 5, 4, 3, 3, 2 };

			for (int i = 0; i < shipNames.Length; i++)
			{
				Console.Clear();
				ShowBoard(board);
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
		private static void placeShipOnBoard(char[,] board, int row, int col, int size, int direction)
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