namespace AONN
{
    public class Organism
    {
        public Organism(double health)
        {
            Health = health;
            Position = new Position(0, 0);
        }

        public Position Position { get; set; }

        public double Health { get; set; }

        public NeuralNetwork.NeuralNetwork NeuralNetwork { get; set; }
    }
}
