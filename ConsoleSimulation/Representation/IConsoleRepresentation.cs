using System;

namespace ConsoleSimulation
{
    public class ColoredChar
    {
        public char Char { get; set; }

        public ConsoleColor Color { get; set; }

        public ColoredChar(ConsoleColor color, char chr)
        {
            Color = color;
            Char = chr;
        }

        public bool HasDefaultColor => Color == IConsoleRepresentation.DefaultColor;
    }

    public interface IConsoleRepresentation
    {
        public const ConsoleColor DefaultColor = ConsoleColor.White;

        public ColoredChar OfDirection(Direction direction);
    }
}
