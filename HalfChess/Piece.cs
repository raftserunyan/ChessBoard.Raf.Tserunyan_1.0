using System;
using System.Collections.Generic;

namespace HalfChess
{
    public class Piece
    {
        public static Board board;
        public List<object> AvailableCells = new List<object>();
        public List<object> EatableCells = new List<object>();

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

        public byte AmountOfMovesToKing;

        public override string ToString()
        {
            return this.Name.Substring(0, 1);
        }

        public void SetEatableCells()
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

                                if (i == this.I && j == this.J)
                                    continue;

                                EatableCells.Add(board.Matrix[i, j]);
                            }
                        }
                        break;
                    }
                case "Queen":
                    {
                        for (int t = 1; t < 8; t++)
                        {
                            if (I - t >= 0)
                                EatableCells.Add(board.Matrix[I - t, J]);
                            if (I + t < 8)
                                EatableCells.Add(board.Matrix[I + t, J]);
                            if (J + t < 8)
                                EatableCells.Add(board.Matrix[I, J + t]);
                            if (J - t >= 0)
                                EatableCells.Add(board.Matrix[I, J - t]);

                            if (I - t >= 0 && J - t >= 0)
                                EatableCells.Add(board.Matrix[I - t, J - t]);
                            if (I - t >= 0 && J + t < 8)
                                EatableCells.Add(board.Matrix[I - t, J + t]);
                            if (I + t < 8 && J + t < 8)
                                EatableCells.Add(board.Matrix[I + t, J + t]);
                            if (I + t < 8 && J - t >= 0)
                                EatableCells.Add(board.Matrix[I + t, J - t]);
                        }
                        break;
                    }
                case "Rook":
                    {
                        for (int t = 1; t < 8; t++)
                        {
                            if (I - t >= 0)
                                EatableCells.Add(board.Matrix[I - t, J]);
                            if (I + t < 8)
                                EatableCells.Add(board.Matrix[I + t, J]);
                            if (J + t < 8)
                                EatableCells.Add(board.Matrix[I, J + t]);
                            if (J - t >= 0)
                                EatableCells.Add(board.Matrix[I, J - t]);
                        }
                        break;
                    }
            }
        }

        public void SetAvailableCells()
        {
            switch (this.Name)
            {
                case "King":
                    {
                        byte reali = I, realj = J;

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

                                if (i == this.I && j == this.J)
                                    continue;

                                board.Matrix[I, J] = ' ';

                                //Cheking if going to that destination is not dangerous
                                bool isShax = false;
                                foreach (Piece item in board.WhitePieces)
                                {
                                    foreach (object cell in item.EatableCells)
                                    {
                                        if (board.Matrix[i, j] == cell)
                                            isShax = true;
                                    }
                                }

                                if (!isShax)
                                    AddToListIfNeeded(board.Matrix[i, j]);
                                board.Matrix[I, J] = this;

                                //Board brd = (Board)board.Clone();
                                //isShax = false;

                                //brd.Matrix[i, j] = this;
                                //brd.Matrix[I, J] = ' ';
                                //I = (byte)i; J = (byte)j;
                                //foreach (Piece pc in brd.WhitePieces)
                                //{
                                //    bool t = true;
                                //    if (t)
                                //    {
                                //        foreach (object cell in pc.AvailableCells)
                                //        {
                                //            if (cell == this)
                                //            {
                                //                isShax = true;
                                //                t = false;
                                //                break;
                                //            }
                                //        }
                                //    }
                                //    else
                                //        break;
                                //}
                            }
                        }

                        break;
                    }
                case "Queen":
                    {
                        bool CanGoRight = true, CanGoLeft = true, CanGoUp = true, CanGoDown = true;
                        bool CanGoUpLeft = true, CanGoUpRight = true, CanGoDownRight = true, CanGoDownLeft = true;
                        for (int t = 1; t < 8; t++)
                        {
                            if (I - t >= 0 && CanGoUp)
                                AddToListIfNeeded(board.Matrix[I - t, J], ref CanGoUp);
                            if (I + t < 8 && CanGoDown)
                                AddToListIfNeeded(board.Matrix[I + t, J], ref CanGoDown);
                            if (J + t < 8 && CanGoRight)
                                AddToListIfNeeded(board.Matrix[I, J + t], ref CanGoRight);
                            if (J - t >= 0 && CanGoLeft)
                                AddToListIfNeeded(board.Matrix[I, J - t], ref CanGoLeft);

                            if (I - t >= 0 && J - t >= 0 && CanGoUpLeft)
                                AddToListIfNeeded(board.Matrix[I - t, J - t], ref CanGoUpLeft);
                            if (I - t >= 0 && J + t < 8 && CanGoUpRight)
                                AddToListIfNeeded(board.Matrix[I - t, J + t], ref CanGoUpRight);
                            if (I + t < 8 && J + t < 8 && CanGoDownRight)
                                AddToListIfNeeded(board.Matrix[I + t, J + t], ref CanGoDownRight);
                            if (I + t < 8 && J - t >= 0 && CanGoDownLeft)
                                AddToListIfNeeded(board.Matrix[I + t, J - t], ref CanGoDownLeft);
                        }
                        break;
                    }
                case "Rook":
                    {
                        bool CanGoRight = true, CanGoLeft = true, CanGoUp = true, CanGoDown = true;
                        for (int t = 1; t < 8; t++)
                        {
                            if (I - t >= 0 && CanGoUp)
                                AddToListIfNeeded(board.Matrix[I - t, J], ref CanGoUp);
                            if (I + t < 8 && CanGoDown)
                                AddToListIfNeeded(board.Matrix[I + t, J], ref CanGoDown);
                            if (J + t < 8 && CanGoRight)
                                AddToListIfNeeded(board.Matrix[I, J + t], ref CanGoRight);
                            if (J - t >= 0 && CanGoLeft)
                                AddToListIfNeeded(board.Matrix[I, J - t], ref CanGoLeft);
                        }
                        break;
                    }
            }
        }

        private void AddToListIfNeeded(object newPiece)
        {
            if (!(newPiece is Piece))
                AvailableCells.Add(newPiece);
            else
            {
                Piece piece = newPiece as Piece;
                if (piece.Color != this.Color)
                    AvailableCells.Add(piece);
            }
        }
        private void AddToListIfNeeded(object newPiece, ref bool direction)
        {
            if (!(newPiece is Piece))
                AvailableCells.Add(newPiece);
            else
            {
                Piece piece = newPiece as Piece;
                if (piece.Color != this.Color)
                    AvailableCells.Add(piece);
                direction = false;
            }
        }

        public void PutOnBoard()
        {
            board.Matrix[this.I, this.J] = this;
        }

        public void Move(string coordinates)
        {
            try
            {
                Piece piece = board.Matrix[I, J] as Piece;

                byte i = (byte)(8 - byte.Parse(coordinates.Substring(0, 1)));
                byte j = (byte)(Convert.ToByte(Convert.ToChar(coordinates.Substring(2, 1).ToUpper())) - 65);

                //Checking for possible mistakes
                if (i == board.Pieces[0].I && j == board.Pieces[0].J)
                        throw new Exception("You've entered the black king's existing coordinates...");


                //Checking if this piece can go there
                bool contains = false;
                foreach (var item in this.AvailableCells)
                {
                    if (item == board.Matrix[i, j])
                    {
                        contains = true;
                        break;
                    }
                }
                if (contains)
                {
                    if (board.Matrix[i, j] is Piece)
                    {
                        Piece pc = board.Matrix[i, j] as Piece;
                        board.Pieces.Remove(pc);

                        if (pc.Color == "White")
                            board.WhitePieces.Remove(pc);
                        else
                            Program.Mate();
                    }

                    board.Matrix[i, j] = this;
                }
                else
                    throw new Exception($"You cant move to the destination {coordinates.ToUpper()}, \n Something's blocking your way or your piece just can't go there.");


                board.Matrix[I, J] = ' ';
                I = i;
                J = j;

                #region Clearing and reloading lists

                foreach (Piece item in board.Pieces)
                {
                    item.EatableCells.Clear();
                    item.SetEatableCells();
                }
                foreach (Piece item in board.Pieces)
                {
                    item.AvailableCells.Clear();
                    item.SetAvailableCells();
                }

                #endregion

                board.Show();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CanEat(Piece pc)
        {
            bool CanKingEatHim = false;

            foreach (object kingCell in board.Pieces[0].AvailableCells)
            {
                if (kingCell == pc)
                {
                    CanKingEatHim = true;
                    break;
                }
            }
            return CanKingEatHim;
        }

        public bool HasSomewhereToGo
        {
            get
            {
                if (AvailableCells.Count > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}