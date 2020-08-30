using System;
using System.Collections.Generic;

namespace HalfChess
{
    class Program
    {
        static Board board;

        static void Main(string[] args)
        {
            board = new Board();
            board.Show();

            while (true)
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

        private static void SystemMakeMove()
        {
            foreach (Piece item in Board.WhitePieces)
            {
                for (int i = 0; i < item.AvailableCells.Count; i++)
                {
                    item.AmountOfMovesToKing = (byte)GetAmountOfMovesToKing(item.AvailableCells[i]);
                }
            }
        }

        Board brd = (Board)board.Clone();
        List<byte> list = new List<byte>();
        private static int GetAmountOfMovesToKing(object cell)
        {
            return 5;
        }
    }
}