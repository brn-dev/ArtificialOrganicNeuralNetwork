using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSimulation
{
    public class ConsoleRepresentation
    {
        public ConsoleRepresentation(char all) : this(all, all, all, all)
        {
        }

        public ConsoleRepresentation(char up, char down, char left, char right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public char Up { get; set; }

        public char Down { get; set; }

        public char Left { get; set; }

        public char Right { get; set; }

        public char OfDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Up,
                Direction.Down => Down,
                Direction.Left => Left,
                Direction.Right => Right,
                _ => throw new ArgumentException()
            };
        }

    }
}
