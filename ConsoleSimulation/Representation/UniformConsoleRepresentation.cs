using System;

namespace ConsoleSimulation.Representation
{
    class UniformConsoleRepresentation : IConsoleRepresentation
    {
        public UniformConsoleRepresentation(char representation, ConsoleColor color = IConsoleRepresentation.DefaultColor)
        {
            Representation = new ColoredChar(color, representation);
        }

        public ColoredChar Representation { get; set; }

        public ColoredChar OfDirection(Direction direction)
        {
            return Representation;
        }
    }
}
