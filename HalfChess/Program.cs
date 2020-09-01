﻿using System;
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

        private static byte i = 1;
        private static byte ind
        {
            get
            {
                return i;
            }
            set
            {
                if (value >= board.WhitePieces.Count)
                    i = 1;
                else if (value < 1)
                    i = 1;
                else
                    i = value;
            }
        }
        private static void SystemMakeMove()
        {

            Piece piece = board.WhitePieces[ind++];

            List<int> IForEachCell = new List<int>();

            foreach (object cell in piece.AvailableCells)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board.Matrix[i, j] == cell)
                        {
                            if (j - board.Pieces[0].J != 1)
                                IForEachCell.Add(Math.Abs(i - board.Pieces[0].I));
                            else
                                IForEachCell.Add(888);
                        }
                    }
                }
            }

            int minI = GetIndexOfMin(IForEachCell);

            bool t = true;
            for (int i = 0; i < 8; i++)
            {
                if (t)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (board.Matrix[i, j] == piece.AvailableCells[minI])
                        {
                            if (minI == 888)
                            {
                                ind++;
                                t = false;
                                break;
                            }
                            piece.Move(i, j);
                            Thread.Sleep(1000);
                            return;
                        }
                    }
                }
            }
        }

        private static int GetIndexOfMin(List<int> list)
        {
            int min = list[0];
            int minI = 0;

            for (int i = 0; i < list.Count; i++)
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