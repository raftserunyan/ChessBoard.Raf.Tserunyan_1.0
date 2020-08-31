using System;
using System.Threading;

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

        private static byte s = 0;
        private static void SystemMakeMove()
        {
            switch (s)
            {
                case 0:
                    {
                        board.Pieces[4].Move("7 h");
                        s++;
                        break;
                    }
                case 1:
                    {
                        board.Pieces[3].Move("8 a");
                        break;
                    }
                default:
                    break;
            }
        }

        private static int GetAmountOfMovesToKing(object cell)
        {
            return 5;
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