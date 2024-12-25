namespace battleShips
{
    public class Check
    {
        public static bool ShipPlacement(char[,] board, int row, int col, int size, int direction)
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

        public static bool ShipsLeft(char[,] board)
        {
            foreach (var cell in board)
                if (cell == 'S' || cell == 'D' || cell == 'B' || cell == 'C' || cell == 'P')
                    return true; // There are still ships on the board

            return false; // No ships left
        }

        public static bool ShipSunk(char[,] board, char shipType)
        {
            foreach (var cell in board)
                if (cell == shipType)
                    return false; // The ship has not been sunk

            return true; // The ship has been sunk
        }

        public static int ShipZonesLeft(char[,] board)
        {
            var count = 0;
            foreach (var cell in board)
                if (cell == 'S' || cell == 'D' || cell == 'B' || cell == 'C' || cell == 'P')
                    count++; // Increment the count if a ship is found

            return count;
        }
    }
}