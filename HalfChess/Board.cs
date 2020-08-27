﻿using System;

namespace HalfChess
{
    public class Board
    {
        public Piece KingBlack, KingWhite, QueenWhite, RookWhiteLeft, RookWhiteRight;
        public object[,] Matrix;

        public object this[byte i, byte j]
        {
            get { return Matrix[i, j]; }
            set { Matrix[i, j] = value; }
        }

        public Board()
        {
            Piece.board = this;
            Matrix = new object[8, 8];
            KingBlack = new Piece("King", "Black", 0, 4);
            KingWhite = new Piece("King", "White", 7, 4);
            QueenWhite = new Piece("Queen", "White", 7, 3);
            RookWhiteLeft = new Piece("Rook", "White", 7, 0);
            RookWhiteRight = new Piece("Rook", "White", 7, 7);
            Create();
        }

        public void Create()
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte j = (byte)(i % 2); j < 8; j += 2)
                {
                    Matrix[i, j] = '*';
                }
            }

            for (byte i = 0; i < 8; i++)
            {
                for (byte j = (byte)(1 - i % 2); j < 8; j += 2)
                {
                    Matrix[i, j] = '#';
                }
            }

            KingBlack.PutOnBoard();
            KingWhite.PutOnBoard();
            QueenWhite.PutOnBoard();
            RookWhiteLeft.PutOnBoard();
            RookWhiteRight.PutOnBoard();
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();

            for (byte i = 0; i < 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();

                for (byte j = 0; j < 8; j++)
                {                    
                    if (Matrix[i, j] is Piece)
                    {
                        Piece piece = Matrix[i, j] as Piece;
                        switch (piece.Color)
                        {
                            case "Black":
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                }
                            case "White":
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                }
                        }
                    }
                    Console.Write($" {Matrix[i, j]} ");
                    Console.ResetColor();

                    if (j < 7)
                        Console.Write("|");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();
                Console.WriteLine();

                if (i == 7)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();
        }
    }
}