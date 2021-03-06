namespace ConsoleSimulation.Representation
{
    public delegate ColoredChar RepresentationComputer(Direction direction);

    public class ComputedConsoleRepresentation : IConsoleRepresentation
    {
        public ComputedConsoleRepresentation(RepresentationComputer representationComputer)
        {
            RepresentationComputer = representationComputer;
        }

        public RepresentationComputer RepresentationComputer { get; set; }

        public ColoredChar OfDirection(Direction direction)
        {
            return RepresentationComputer(direction);
        }
    }
}
