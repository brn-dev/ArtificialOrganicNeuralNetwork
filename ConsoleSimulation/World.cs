using System.Collections.Generic;

namespace ConsoleSimulation
{
    public class World
    {
        public IList<IConsoleEntity> Entities { get; set; } = new List<IConsoleEntity>();

    }
}
