using System;

namespace HalfChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Show();

            bool InputSucceed = false;

            while (!InputSucceed)
            {
                Console.WriteLine();
                Console.Write("Enter new coordinates for the black king (example: 5 G): ");
                string coordinates = Console.ReadLine();

                try
                {
                    byte i = (byte)(8 - byte.Parse(coordinates.Substring(0, 1)));
                    byte j = (byte)(Convert.ToByte(Convert.ToChar(coordinates.Substring(2, 1))) - 65);
                    if (i == board.KingBlack.I && j == board.KingBlack.J)
                    {
                        Console.WriteLine("You've entered the black king's existing coordinates...");
                        continue;
                    }
                    board.KingBlack.Move(i, j);
                    Console.Clear();
                    board.Show();
                    Console.WriteLine(Convert.ToInt32('H'));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}