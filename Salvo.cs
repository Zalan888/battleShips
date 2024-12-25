using System;

namespace battleShips
{
    public class Salvo
    {
        private static int player1Shots = 5; // default amount of shots 
        private static int player2Shots = 5; // default amount of shots 

        public static void Loop()
        {
            while (true)
            {
                TurnBasedAttack(MainClass.player1Name, MainClass.player1Attacks, MainClass.player2Map,
                    MainClass.player1Map, player1Shots);
                TurnBasedAttack(MainClass.player2Name, MainClass.player2Attacks, MainClass.player1Map,
                    MainClass.player2Map, player2Shots);
            }
        }

        private static void TurnBasedAttack(string playerName, char[,] playerAttacks, char[,] opponentMap,
            char[,] playerMap, int playersShots)
        {
            int row = 0, col = 0;
            for (var i = 0; i < playersShots; i++)
            {
                if (!Check.ShipsLeft(opponentMap)) // check if there is a winner
                {
                    Console.Clear();
                    Console.WriteLine($"{playerName} wins! All ships of the opponent are sunk.");
                    Console.WriteLine("Press any key to return to menu.");
                    Console.ReadKey();
                    MainClass.Menu();
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
                } while (row < 0 || row >= MainClass.mapSize); // check if the input is valid

                do
                {
                    Console.Write("Enter column to attack (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out col)) // get the column input
                        col -= 1; // 0-based index conversion
                } while (col < 0 || col >= MainClass.mapSize); // check if the input is valid

                if (Check.WasTried(row, col, opponentMap))
                {
                    Console.WriteLine("You have already tried this location. Try again.");
                    i--; // Decrement the counter to allow the player to try again
                }
                else
                {
                    Attack(row, col, opponentMap, playerAttacks);
                }
            }

            MainClass.playerTurn = MainClass.playerTurn == 1 ? 2 : 1; // Switch the player turn
            Console.Clear();
            Console.WriteLine("End of your turn. Press any button");
            Console.ReadKey();
        }

        private static void Attack(int row, int col, char[,] enemyPlayerMap, char[,] playerAttackMap)
        {
            // Check if there is a ship at the target location
            if (enemyPlayerMap[row, col] == 'S' || enemyPlayerMap[row, col] == 'D' || enemyPlayerMap[row, col] == 'B' ||
                enemyPlayerMap[row, col] == 'C' || enemyPlayerMap[row, col] == 'P')
            {
                var shipType = enemyPlayerMap[row, col];
                enemyPlayerMap[row, col] = 'X'; // Mark as hit
                playerAttackMap[row, col] = 'X';
                Console.WriteLine("Hit!");
                if (Check.ShipSunk(enemyPlayerMap, shipType))
                {
                    Console.WriteLine("Ship sunk!");
                    if (playerAttackMap == MainClass.player1Attacks)
                        player2Shots -= 1;
                    else
                        player1Shots -= 1;
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                playerAttackMap[row, col] = 'O'; // Mark as missed
                Console.WriteLine("Miss!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}