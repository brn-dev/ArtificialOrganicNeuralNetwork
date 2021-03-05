using System;

namespace ConsoleSimulation
{

    public interface IConsoleEntity
    {
        public Vector2i Position { get; set; }

        public Direction Rotation { get; set; }

        public EntityType EntityType { get; }

        public ConsoleRepresentation Representation { get; }

    }

    public static class IConsoleEntityHelper
    {
        public static char GetCurrentRepresentation(this IConsoleEntity consoleEntity)
        {
            return consoleEntity.Representation.OfDirection(consoleEntity.Rotation);
        }

        public static void RotateCounterClockwise(this IConsoleEntity consoleEntity)
        {
            consoleEntity.Rotation = consoleEntity.Rotation switch
            {
                Direction.Up => Direction.Left,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                Direction.Right => Direction.Up,
                _ => throw new ArgumentException(),
            };
        }

        public static void RotateClockwise(this IConsoleEntity consoleEntity)
        {
            consoleEntity.Rotation = consoleEntity.Rotation switch
            {
                Direction.Up => Direction.Right,
                Direction.Down => Direction.Left,
                Direction.Right => Direction.Down,
                Direction.Left => Direction.Up,
                _ => throw new ArgumentException(),
            };
        }

        public static bool IsNextTo(this IConsoleEntity e1, IConsoleEntity e2)
        {
            return e1.Position.IsNextTo(e2.Position);
        }
    }

}
