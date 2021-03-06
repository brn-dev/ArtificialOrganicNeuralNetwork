using AONN;
using ConsoleSimulation.Entities;

namespace ConsoleSimulation
{
    public class ConsoleOrganism : AbstractOrganism, ITickableConsoleEntity
    {
        /// momentum relative to the organisms rotation, momentum is rotated if organsim rotates
        private readonly Vector2d _momentum = new Vector2d();
        private double _angularMomementum = 0;

        public ConsoleOrganism()
        {
        }

        public EntityType EntityType { get; } = EntityType.Subject;

        public IConsoleRepresentation Representation { get; } = new DirectionalConsoleRepresentation('^', 'v', '<', '>');

        public Direction Rotation { get; set; } = Direction.Up;

        public Vector2i Position { get; set; } = new Vector2i();

        public World World { get; set; }

        public long CurrentTick { get; set; } = 0L;

        public int FoodCollected { get; set; }

        public void AddMomentum(Vector2d m)
        {
            _momentum.Add(m);
        }

        public void AddAngularMomentum(double am)
        {
            _angularMomementum += am;
        }

        public override void Tick(long tick)
        {
            CurrentTick = tick;

            base.Tick(tick);

            if (_momentum.X >= 1 || _momentum.X <= -1 || 
                _momentum.Y >= 1 || _momentum.Y <= -1)
            {
                Move();
            }

            if (_angularMomementum >= 1 || _angularMomementum <= -1)
            {
                Rotate();
            }

            foreach (var entity in World.Entities)
            {
                if (entity is FoodEntity foodEntity && foodEntity.IsNextTo(this) && !foodEntity.IsEaten)
                {
                    FoodCollected++;
                    //World.RemoveEntity(entity);
                    ((FoodEntity)entity).Eat();
                }
            }
        }

        private void Move()
        {
            Vector2i v;

            if (_momentum.X >= 1)
            {
                v = new Vector2i(1, 0);
            }
            else if (_momentum.X <= -1)
            {
                v = new Vector2i(-1, 0);
            }
            else if (_momentum.Y >= 1)
            {
                v = new Vector2i(0, 1);
            }
            else
            {
                v = new Vector2i(0, -1);
            }

            switch (Rotation)
            {
                case Direction.Left:
                    v.RotateCounterClockwise();
                    break;
                case Direction.Right:
                    v.RotateClockwise();
                    break;
                case Direction.Down:
                    v.Reverse();
                    break;
            }

            Position.Add(v);
            _momentum.Reset();
        }

        private void Rotate()
        {
            if (_angularMomementum >= 1)
            {
                this.RotateClockwise();
                _momentum.RotateClockwise();
            }
            else if (_angularMomementum <= -1)
            {
                this.RotateCounterClockwise();
                _momentum.RotateCounterClockwise();
            }
            _angularMomementum = 0;
        }
    }
}
