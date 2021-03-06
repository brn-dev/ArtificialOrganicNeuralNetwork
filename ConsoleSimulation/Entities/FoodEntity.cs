using ConsoleSimulation.Representation;
using System;

namespace ConsoleSimulation.Entities
{
    class FoodEntity : IConsoleEntity
    {
        private readonly ColoredChar _normalRepresentation = new ColoredChar(IConsoleRepresentation.DefaultColor, '#');
        private readonly ColoredChar _eatenRepresentation = new ColoredChar(ConsoleColor.DarkGray, '#');

        public FoodEntity(Vector2i position)
        {
            Position = position;
            Representation = new ComputedConsoleRepresentation(d => IsEaten ? _eatenRepresentation : _normalRepresentation);
        }

        public Vector2i Position { get; set; }

        public Direction Rotation { get; set; } = Direction.Up;

        public EntityType EntityType { get; } = EntityType.Food;
        
        public IConsoleRepresentation Representation { get; }

        public bool IsEaten { get; set; } = false;

        public void Eat()
        {
            IsEaten = true;
        }
    }
}
