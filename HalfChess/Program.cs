using System;
using System.Threading;
using System.Collections.Generic;

namespace HalfChess
{
    class Program
    {
        static Board board;
        static bool isMate = false;

        static void Main(string[] args)
        {
            board = new Board();
            board.Show();

            while (!isMate)
            {
                //Checking for mate
                if (!board.Pieces[0].HasSomewhereToGo)
                    Mate();

                if (!isMate)
                {
                    Console.WriteLine();
                    Console.Write("Enter new coordinates for the black king (example: 7 F): ");
                    string coordinates = Console.ReadLine();

                    try
                    {
                        board.Pieces[0].Move(coordinates);

                        Thread.Sleep(1200);
                        SystemMakeMove();

                        //Check for shakh
                        if (IsShakh())
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("System> Shakh!");
                            Console.ResetColor();
                        }

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
        }

        public static void Mate()
        {
            isMate = true;

            board.Pieces[0].AvailableCells.Clear();
            Console.Clear();
            board.Show();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MATE!");
            Console.ReadKey();
        }

        private static void SystemMakeMove()
        {
            Random rnd = new Random();
            byte ind = (byte)rnd.Next(1, board.WhitePieces.Count);

            Piece piece = board.WhitePieces[ind] as Piece;

            List<int> MovesForEachCell = new List<int>();

            foreach (object cell in piece.AvailableCells)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board.Matrix[i, j] == cell)
                        {
                            MovesForEachCell.Add(Math.Abs(i - board.Pieces[0].I));
                        }
                    }
                }
            }

            int minI = GetIndexOfMin(MovesForEachCell);


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board.Matrix[i, j] == piece.AvailableCells[minI])
                    {
                        piece.Move(i, j);
                        Thread.Sleep(1000);
                        return;
                    }
                }
            }

        }

        private static int GetIndexOfMin(List<int> list)
        {
            int min = list[0];
            int minI = 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    minI = i;
                }
            }

            return minI;
        }

        private static bool IsShakh()
        {
            foreach (Piece piece in board.Pieces)
            {
                if (piece.CanEat(board.Pieces[0]))
                    return true;
            }
            return false;
        }
    }
}