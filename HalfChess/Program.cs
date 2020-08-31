using System;
using System.Collections.Generic;
using System.Threading;

namespace HalfChess
{
    class Program
    {
        static Board board;

        static void Main(string[] args)
        {
            board = new Board();
            board.Show();

            int s = 0;
            bool isMate = false;
            while (!isMate)
            {
                //Checking for mate
                foreach (Piece pi in board.WhitePieces)
                {
                    foreach (object cell in pi.AvailableCells)
                    {
                        if (cell == board.KingBlack)
                        {
                            isMate = true;

                            board.KingBlack.AvailableCells.Clear();
                            Console.Clear();
                            board.Show();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("MATE!");
                            Console.ReadKey();
                        }
                    }
                }

                //Checking for mate
                if (board.KingBlack.AvailableCells.Count < 1)
                {
                    isMate = true;

                    board.KingBlack.AvailableCells.Clear();
                    Console.Clear();
                    board.Show();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("MATE!");
                    Console.ReadKey();
                }


                if (!isMate)
                {
                    Console.WriteLine();
                    Console.Write("Enter new coordinates for the black king (example: 7 F): ");
                    string coordinates = Console.ReadLine();

                    try
                    {
                        board.KingBlack.Move(coordinates);
                        //Console.Clear();
                        //board.Show();

                        Thread.Sleep(1000);

                        switch (s)
                        {
                            case 0:
                                {
                                    board.RookWhiteRight.Move("7 h");
                                    s++;
                                    break;
                                }
                            case 1:
                                {
                                    board.RookWhiteLeft.Move("8 a");
                                    break;
                                }
                            default:
                                break;
                        }

                        //Console.Clear();
                        //board.Show();
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

        private static void SystemMakeMove()
        {
           
        }

        private static int GetAmountOfMovesToKing(object cell)
        {
            return 5;
        }
    }
}