using System;

namespace ConsoleSimulation
{
    public class DirectionalConsoleRepresentation : IConsoleRepresentation
    {
        public DirectionalConsoleRepresentation(char up, char down, char left, char right, ConsoleColor color = IConsoleRepresentation.DefaultColor)
        {
            Up = new ColoredChar(color, up);
            Down = new ColoredChar(color, down);
            Left = new ColoredChar(color, left);
            Right = new ColoredChar(color, right);
        }

        public ColoredChar Up { get; set; }

        public ColoredChar Down { get; set; }

        public ColoredChar Left { get; set; }

        public ColoredChar Right { get; set; }

        public ColoredChar OfDirection(Direction direction)
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
