using System.Collections.Generic;

namespace ConsoleSimulation
{
    class FoodEntity : IConsoleEntity
    {
        public FoodEntity(Vector2i position)
        {
            Position = position;
        }

        public Vector2i Position { get; set; }

        public Direction Rotation { get; set; } = Direction.Up;

        public EntityType EntityType { get; } = EntityType.Food;

        public ConsoleRepresentation Representation { get; } = new ConsoleRepresentation('#');
    }
}
