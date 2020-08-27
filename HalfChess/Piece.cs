using System;

namespace HalfChess
{
    public class Piece
    {
        public static Board board;

        public Piece(string name, string color, int _i, int _j)
        {
            Name = name;
            Color = color;
            i = _i;
            j = _j;
        }

        public string Color { get; set; }
        public string Name { get; set; }
        public char CellSign { get; set; }

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
            return this.Name.Substring(0,1);
        }

        public void PutOnBoard()
        {
            CellSign = (char)board.Matrix[this.I, this.J];
            board.Matrix[this.I, this.J] = this;
        }

        public void Move(byte _i, byte _j)
        {
            try
            {
                Piece piece = board.Matrix[I, J] as Piece;

                char tmpSign = (char)board.Matrix[_i, _j];

                board.Matrix[_i, _j] = piece;

                board.Matrix[I, J] = CellSign;
                CellSign = tmpSign;
                I = _i;
                J = _j;
                board.Show();
            }
            catch (Exception e)
            {
                throw e;//new Exception("Duq nermucel eq nuyn koordinatnery");
            }
        }
    }
}