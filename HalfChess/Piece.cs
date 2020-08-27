using System;
using System.Collections.Generic;

namespace HalfChess
{
    public class Piece
    {
        public static Board board;
        public List<object> AvailableCells = new List<object>();

        public Piece(string name, string color, int _i, int _j)
        {
            Name = name;
            Color = color;
            i = _i;
            j = _j;
        }

        public string Color { get; set; }
        public string Name { get; set; }

        public byte I;
        private int i
        {
            get { return I; }
            set
            {
                if (value < 0)
                    I = 0;
                else if (value > 7)
                    I = 7;
                else
                    I = (byte)value;
            }
        }

        public byte J;
        private int j
        {
            get { return J; }
            set
            {
                if (value < 0)
                    J = 0;
                else if (value > 7)
                    J = 7;
                else
                    J = (byte)value;
            }
        }

        public override string ToString()
        {
            return this.Name.Substring(0, 1);
        }

        public void SetAvailableCells()
        {
            switch (this.Name)
            {
                case "King":
                    {
                        for (int i = I - 1; i < I + 2; i++)
                        {
                            if (i < 0)
                                continue;
                            else if (i == board.Matrix.GetLength(0))
                                break;

                            for (int j = J - 1; j < J + 2; j++)
                            {
                                if (j < 0)
                                    continue;
                                else if (j == board.Matrix.GetLength(1))
                                    break;

                                AvailableCells.Add(board.Matrix[i, j]);
                            }
                        }
                        break;
                    }
                case "Queen":
                    {
                        
                        for (int t = 0; t < 8; t++)
                        {
                            AvailableCells.Add(board.Matrix[t, J]);
                            AvailableCells.Add(board.Matrix[I, t]);

                            if (I - t >= 0 && J - t >= 0)
                              AvailableCells.Add(board.Matrix[I - t, J - t]);
                            if (I - t >= 0 && J + t < 8)
                              AvailableCells.Add(board.Matrix[I - t, J + t]);
                            if (I + t < 8 && J + t < 8)
                                AvailableCells.Add(board.Matrix[I + t, J + t]);
                            if (I + t < 8 && J - t >= 0)
                                AvailableCells.Add(board.Matrix[I + t, J - t]);
                        }
                        
                        break;
                    }
                case "Rook":
                    {
                        for (int t = 0; t < 8; t++)
                        {
                            AvailableCells.Add(board.Matrix[t, J]);
                            AvailableCells.Add(board.Matrix[I, t]);
                        }
                            break;
                    }
            }
        }

        public void PutOnBoard()
        {
            board.Matrix[this.I, this.J] = this;
            SetAvailableCells();
        }

        public void Move(byte _i, byte _j)
        {
            try
            {
                Piece piece = board.Matrix[I, J] as Piece;

                board.Matrix[_i, _j] = piece;
                board.Matrix[i, j] = ' ';
                I = _i;
                J = _j;

                AvailableCells.Clear();
                SetAvailableCells();
                board.Show();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}