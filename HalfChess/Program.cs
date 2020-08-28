using System;
using System.Collections.Generic;

namespace HalfChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Show();

            bool InputSucceed = false;

            while (!InputSucceed)
            {
                Console.WriteLine();
                Console.Write("Enter new coordinates for the black king (example: 5 G): ");
                string coordinates = Console.ReadLine();

                try
                {
                    board.KingBlack.Move(coordinates);
                    Console.Clear();
                    board.Show();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }
        }

        public static void SystemMakeMove()
        {
            List<Piece> pieces = new List<Piece>();
            
        }
    }
}