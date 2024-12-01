using System;

namespace battleShips
{
	internal class Program
	{
		static string[] xCords = {" A ", " B ", " C ", " D ", " E ", " F ", " G ", " H ", " I ", " J "};

		public static void mapDraw()
		{
			for (int i = 0; i < 11; i++)
			{
				for (int j = 0; j < 10; j++) 
				{
					if (i == 0)
					{
						Console.Write(xCords[j]);
					}
					else 
					{
                        Console.Write(" O ");
					}
                }
                Console.WriteLine();
                Console.WriteLine(" - - - - - - - - - - - - - - - ");
			}
		}
		static void Main(string[] args)
		{
			mapDraw();
		}
	}
}
