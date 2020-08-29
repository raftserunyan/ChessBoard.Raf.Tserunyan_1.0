using System;
using System.Collections.Generic;

namespace HalfChess
{
    class Program
    {
        static Board board;
        public static List<Piece> WhitePieces;

        static void Main(string[] args)
        {
            board = new Board();
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

        

        private static void SystemMakeMove()
        {
            

            foreach (Piece item in WhitePieces)
            {

            }
        }

        //Board brd = (Board)board.Clone();
        //private static int GetAmountOfMovesToKing(Piece piece)
        //{
        //    piece.AvailableCells[0]
        //}

    }
}