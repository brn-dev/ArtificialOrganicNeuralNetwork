using System.Collections.Generic;

namespace ConsoleSimulation
{
    class World
    {
        public IList<IConsoleEntity> Entities { get; set; } = new List<IConsoleEntity>();

    }
}
