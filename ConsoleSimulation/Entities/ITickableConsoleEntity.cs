namespace ConsoleSimulation.Entities
{
    public interface ITickableConsoleEntity : IConsoleEntity
    {
        public void Tick(long tick);
    }
}
