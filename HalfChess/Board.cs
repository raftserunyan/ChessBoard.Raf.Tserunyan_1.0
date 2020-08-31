using System;
using System.Collections.Generic;

namespace HalfChess
{
    public class Board : ICloneable
    {
        public Piece KingBlack, KingWhite, QueenWhite, RookWhiteLeft, RookWhiteRight;
        public object[,] Matrix;

        public List<Piece> WhitePieces;

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
            RookWhiteLeft = new Piece("Rook", "White", 7, 0);
            RookWhiteRight = new Piece("Rook", "White", 7, 7);
            QueenWhite = new Piece("Queen", "White", 7, 3);

            InitializeWhitePieces();

            Create();
        }

        public void Create()
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    Matrix[i, j] = ' ';
                }
            }

            RookWhiteLeft.PutOnBoard();
            RookWhiteRight.PutOnBoard();
            QueenWhite.PutOnBoard();
            KingWhite.PutOnBoard();
            KingBlack.PutOnBoard();

            RookWhiteLeft.SetEatableCells();
            RookWhiteRight.SetEatableCells();
            QueenWhite.SetEatableCells();
            KingWhite.SetEatableCells();
            KingBlack.SetEatableCells();

            RookWhiteLeft.SetAvailableCells();
            RookWhiteRight.SetAvailableCells();
            QueenWhite.SetAvailableCells();
            KingWhite.SetAvailableCells();
            KingBlack.SetAvailableCells();



        }

        public void Show()
        {
            Console.Clear();

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

                //Some loop for choosing colors
                for (byte j = 0; j < 8; j++)
                {
                    if (!(Matrix[i, j] is Piece))
                    {
                        switch ((i + j) % 2)
                        {
                            case 0:
                                {
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    break;
                                }
                            case 1:
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Piece piece = Matrix[i, j] as Piece;
                        switch (piece.Color)
                        {
                            case "Black":
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    break;
                                }
                            case "White":
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    break;
                                }
                        }
                    }

                    //Coloring available cells
                    foreach (var item in KingBlack.AvailableCells)
                    {
                        if (Matrix[i, j] == item)
                            Console.BackgroundColor = ConsoleColor.Green;
                    }

                    //Choosing the right ForeColor for a better UI;
                    switch (Console.BackgroundColor)
                    {
                        case ConsoleColor.Green:
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            }
                        case ConsoleColor.DarkGreen:
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            }
                        default:
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
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

        public void InitializeWhitePieces()
        {
            WhitePieces = new List<Piece>
            {
                KingWhite,
                QueenWhite,
                RookWhiteLeft,
                RookWhiteRight
            };
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}