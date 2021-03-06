using ConsoleSimulation.Entities;
using System.Collections.Generic;

namespace ConsoleSimulation
{
    public class World
    {
        private readonly List<IConsoleEntity> _entities = new List<IConsoleEntity>();
        private readonly List<ITickableConsoleEntity> _tickableEntities = new List<ITickableConsoleEntity>();
        private readonly List<IConsoleEntity> _entitiesToRemove = new List<IConsoleEntity>();

        public IReadOnlyList<IConsoleEntity> Entities => _entities;

        public IReadOnlyList<ITickableConsoleEntity> TickableConsoleEntities => _tickableEntities;

        public void Tick(long tick)
        {
            var length = _tickableEntities.Count;
            for (int i = 0; i < length; i++)
            {
                _tickableEntities[i].Tick(tick);
            }

            var removeLength = _entitiesToRemove.Count;
            if (removeLength > 0)
            {
                for (int i = 0; i < removeLength; i++)
                {
                    ActuallyRemove(_entitiesToRemove[i]);
                }
                _entitiesToRemove.Clear();
            }
        }

        public void AddEntity(IConsoleEntity entity)
        {
            _entities.Add(entity);

            if (entity is ITickableConsoleEntity tickableEntity)
            {
                _tickableEntities.Add(tickableEntity);
            }
        }

        public void RemoveEntity(IConsoleEntity entity)
        {
            _entitiesToRemove.Add(entity);
        } 

        private void ActuallyRemove(IConsoleEntity entity)
        {
            _entities.Remove(entity);

            if (entity is ITickableConsoleEntity tickableEntity)
            {
                _tickableEntities.Remove(tickableEntity);
            }
        }

    }
}
